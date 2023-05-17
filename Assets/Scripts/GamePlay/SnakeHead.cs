using System;
using UnityEngine;

namespace GamePlay
{
    public class SnakeHead : MonoBehaviour
    {
        public static event Action OnFoodEat;
        public static event Action<int> OnFoodEatPoints; 

        private void OnTriggerEnter(Collider other)
        {
            var foodObject = other.gameObject.GetComponent<FoodObject>();
            if (foodObject!= null)
            {
                Debug.Log("Score increase");
                OnFoodEatPoints?.Invoke(foodObject.points);
                OnFoodEat?.Invoke();
                Destroy(other.gameObject);
            }
        }
    }
}
