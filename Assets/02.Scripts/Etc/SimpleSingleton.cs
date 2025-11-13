using UnityEngine;

public class SimpleSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static bool IsManagerExist()
    {
        if(instance == null)
        {
            Debug.LogWarning($"{nameof(T)} is not Exist");
        }

        return instance != null;
    }
}
