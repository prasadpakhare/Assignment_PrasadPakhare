using Core;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    private PlayerControls _playerControls;
    private Camera _mainCamera;

    #region Events

    public delegate void StartTouch(Vector2 position, float time);

    public event StartTouch OnStartTouch;

    public delegate void EndTouch(Vector2 position, float time);

    public event StartTouch OnEndTouch;

    #endregion

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
        _mainCamera = Camera.main;
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void Start()
    {
        _playerControls.Touch.PrimaryContact.started += context => StartTouchPrimary(context);
        _playerControls.Touch.PrimaryContact.canceled += context => EndTouchPrimary(context);
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (_mainCamera)
            OnEndTouch?.Invoke(
                Utils.ScreenToWorld(_mainCamera, _playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()),
                (float) context.time);
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (_mainCamera)
            OnStartTouch?.Invoke(
                Utils.ScreenToWorld(_mainCamera, _playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()),
                (float) context.startTime);
    }
}