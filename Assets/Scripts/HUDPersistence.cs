using UnityEngine;

public class HUDPersistence : MonoBehaviour
{
    static HUDPersistence instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}