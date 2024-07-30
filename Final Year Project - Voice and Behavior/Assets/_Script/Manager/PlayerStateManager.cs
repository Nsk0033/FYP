using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager instance;

    public bool CanMove { get; set; } = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCanMove(bool value)
    {
        CanMove = value;
        if (ThirdPersonController.instance != null)
        {
            ThirdPersonController.instance.CanMove = value;
        }
        else
        {
            Debug.LogError("ThirdPersonController instance is not found.");
        }
    }
}
