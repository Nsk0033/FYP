using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        // Ensure that this GameObject is not destroyed when loading a new scene
        DontDestroyOnLoad(gameObject);
    }
}