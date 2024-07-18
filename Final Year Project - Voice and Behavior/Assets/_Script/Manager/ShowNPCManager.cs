using UnityEngine;

public class ShowNPCManager : MonoBehaviour
{
    public static ShowNPCManager instance { get; private set; }

    [SerializeField] private GameObject npcToShow;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Optional: If you want the ShowNPCManager to persist across scenes
    }

    public void ShowNPC()
    {
        if (npcToShow != null)
        {
            npcToShow.SetActive(true);
        }
    }
}
