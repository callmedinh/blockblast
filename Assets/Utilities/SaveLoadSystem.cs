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
    }
    public class SaveLoadSystem : MonoBehaviour
    {
        private string _path;
        private void Awake()
        {
            _path = Application.persistentDataPath + "/grade.json";
        }

        public void Save(DataUser dataUser)
        {
            string json = JsonUtility.ToJson(dataUser);
            File.WriteAllText(_path, json);
        }

        public void Load()
        {
            string json = File.ReadAllText(_path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
        }
    }
}