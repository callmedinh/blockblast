using System;
using System.IO;
using System.Net;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;

namespace DefaultNamespace.Utilities
{
    [Serializable]
    public class DataUser 
    {
        public int grade;

        public DataUser(int grade)
        {
            this.grade = grade;
        }
    }
    public class SaveLoadSystem : Singleton<SaveLoadSystem>
    {
        private static string FileName => "grade.json";
        private static string FullPath => Path.Combine(Application.persistentDataPath, FileName);

        public void Save(DataUser dataUser)
        {
            string json = JsonUtility.ToJson(dataUser);
            Debug.Log("Saving to: " + FullPath);
            Debug.Log("Data: " + json);
            File.WriteAllText(FullPath, json);
        }

        public DataUser Load()
        {
            if (!File.Exists(FullPath))
            {
                Debug.LogWarning("Save file not found. Returning default.");
                return new DataUser(0);
            }
            string json = File.ReadAllText(FullPath);
            return JsonUtility.FromJson<DataUser>(json);
        }
    }
}