using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GamePlay
{
    public class SceneLoader : MonoBehaviour
    {
        public Button button;
        public SceneName loadScene;
        private readonly Dictionary<SceneName, string> _sceneDictionary = new ();

        private void Awake()
        {
            _sceneDictionary.Add(SceneName.GAME_SCENE,"GameScene");
            _sceneDictionary.Add(SceneName.START_SCENE,"StartScene");
        }

        void Start()
        {
           if(button) button.onClick.AddListener(() => SceneManager.LoadScene(_sceneDictionary[loadScene]));
           else
           {
               Debug.LogError("button reference is missing");
           }
        }
        
    }

    public enum SceneName
    {
        START_SCENE,
        GAME_SCENE
    }
    
}