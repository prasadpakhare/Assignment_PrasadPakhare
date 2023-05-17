using UnityEngine;

namespace Core
{
    public class InputDetection : MonoBehaviour
    {
        private const float MaxSwipeTime = 0.75f;


        private const float MinSwipeDistance = 0.17f;

        public static bool rightSwipe;
        public static bool leftSwipe;
        public static bool upSwipe;
        public static bool downSwipe;
        

        Vector2 startPos;
        float startTime;

        public void Update()
        {
            rightSwipe = false;
            leftSwipe = false;
            upSwipe = false;
            downSwipe = false;

            if (Input.touches.Length > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    startPos = new Vector2(touch.position.x / (float) Screen.width,
                        touch.position.y / (float) Screen.width);
                    startTime = Time.time;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    if (Time.time - startTime > MaxSwipeTime) // long press ignore
                        return;

                    Vector2 endPos = new Vector2(touch.position.x / (float) Screen.width,
                        touch.position.y / (float) Screen.width);

                    Vector2 swipe = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);

                    if (swipe.magnitude < MinSwipeDistance) // short swipe ignore
                        return;

                    if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
                    {
                        // Horizontal swipe detection
                        if (swipe.x > 0)
                        {
                            Debug.Log("Right swipe");
                            rightSwipe = true;
                        }
                        else
                        {
                            Debug.Log("left swipe");
                            leftSwipe = true;
                        }
                    }
                    else
                    {
                        // Vertical swipe detection
                        if (swipe.y > 0)
                        {
                            Debug.Log("Up swipe");
                            upSwipe = true;
                        }
                        else
                        {
                            Debug.Log("down swipe");
                            downSwipe = true;
                        }
                    }
                }
            }
        }
    }
}