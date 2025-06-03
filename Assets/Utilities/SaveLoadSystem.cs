using System;
using System.IO;
using Data;
using UnityEngine;

namespace Utilities
{
    public class SaveLoadSystem : Singleton<SaveLoadSystem>
    {
        private static string FullPath => Path.Combine(Application.persistentDataPath, GameConstants.FileDataName);

        public void SaveGameData(GameData gameData)
        {
            string json = JsonUtility.ToJson(gameData);
            Debug.Log("Saving to: " + FullPath);
            Debug.Log("Data: " + json);
            File.WriteAllText(FullPath, json);
        }

        public GameData LoadGameData()
        {
            if (!File.Exists(FullPath))
            {
                Debug.LogWarning("SaveGameData file not found. Returning default.");
                return new GameData();
            }
            string json = File.ReadAllText(FullPath);
            return JsonUtility.FromJson<GameData>(json);
        }
        public void DeleteSaveData()
        {
            if (File.Exists(FullPath))
            {
                File.Delete(FullPath);
                Debug.Log("SaveGameData data deleted.");
            }
            else
            {
                Debug.LogWarning("No save file to delete.");
            }
        }
    }
}