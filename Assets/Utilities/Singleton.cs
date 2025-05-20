using UnityEngine;

public class Singleton <T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            _instance = GameObject.FindAnyObjectByType<T>();
            if (_instance == null)
            {
                GameObject managerObj = GameObject.Find("Manager");
                GameObject obj = new GameObject();
                obj.transform.SetParent(managerObj.transform);
                _instance = obj.AddComponent<T>();
                obj.name = typeof(T).Name;
            }
            return _instance;
        }
    }
}
