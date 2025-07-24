using System.IO;
using UnityEngine;
using Utilities;

namespace Data
{
    public class SaveLoadSystem : Singleton<SaveLoadSystem>
    {
        private static string FullPath => Path.Combine(Application.persistentDataPath, GameConstants.FileDataName);

        public void SaveGameData(GameDataContainer gameData)
        {
            string json = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(FullPath, json);
        }

        public GameDataContainer LoadGameData()
        {
            if (!File.Exists(FullPath))
            {
                return new GameDataContainer();
            }
            string json = File.ReadAllText(FullPath);
            return JsonUtility.FromJson<GameDataContainer>(json);
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