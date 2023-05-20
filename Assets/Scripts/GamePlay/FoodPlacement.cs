using Core;
using JsonParser;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay
{
    public class FoodPlacement : Grid
    {
    
        public GameObject foodPrefab;

        private GameObject _spawnFood;

        private void Start()
        {
            SpawnFood();
        }

        private void OnEnable()
        {
            SnakeHead.OnFoodEat += SpawnFood;
        }

        private void OnDisable()
        {
            SnakeHead.OnFoodEat -= SpawnFood;
        }

        private void SpawnFood()
        {
            Vector2Int randomCell = GetRandomUnoccupiedPosition();
            if (randomCell != Vector2Int.zero)
            {
                Vector3 foodPosition = CellToWorldPosition(randomCell);

                Constants.previousColor = Constants.currentColor;
            
                _spawnFood = Instantiate(foodPrefab, foodPosition, Quaternion.identity);
                var foodObject = _spawnFood.GetComponent<FoodObject>();
                var randomNumber = Random.Range(0, FoodParser.Instance().food.foodData.Count);
            
                // Assign food points from json 
                foodObject.points = FoodParser.Instance().food.foodData[randomNumber].points;
                foodObject.GetComponent<Renderer>().material.color = FoodParser.Instance().food.foodData[randomNumber].color;
            
                Constants.currentColor = (Colors)randomNumber;
                // Debug.Log($"Spawn food color is {Constants.currentColor}");
            }
        }
        
    }
}
