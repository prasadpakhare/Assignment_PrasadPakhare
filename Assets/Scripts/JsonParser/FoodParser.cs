using System;
using System.Collections.Generic;
using System.IO;
using Core;
using Newtonsoft.Json;
using UnityEngine;

namespace JsonParser
{
    public class FoodParser : Singleton<FoodParser>
    {
        public FoodJsonProperties food;

        // public string fileName = "Assets/Scripts/JsonParser/foodData.json";
        public TextAsset textAsset;

        private void Awake()
        {
#if UNITY_EDITOR && UNITY_iOS
            string jsonString = File.ReadAllText(fileName);
            food = CreateFromJson(jsonString);

            // Overwrite the player.json file with new data
            jsonString = SaveToString(food);
            File.WriteAllText(fileName, jsonString);
#else
            Debug.Log(textAsset.text);
            food = CreateFromJson(textAsset.text);
#endif
        }


        private static FoodJsonProperties CreateFromJson(string jsonString)
        {
            return JsonUtility.FromJson<FoodJsonProperties>(jsonString);
        }

        private static string SaveToString(FoodJsonProperties food)
        {
            return JsonUtility.ToJson(food, true);
        }
    }

    [Serializable]
    public class Item
    {
        public Color color;
        public int points;
    }

    [Serializable]
    public class FoodJsonProperties
    {
        public List<Item> foodData;
    }

    // colors sequence is places as per json objects color sequence
    [Serializable]
    public enum Colors
    {
        NONE = -1,
        BLUE = 0,
        RED = 1
    }
}