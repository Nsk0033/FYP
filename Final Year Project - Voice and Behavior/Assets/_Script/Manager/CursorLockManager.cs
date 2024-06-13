using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class CursorLockManager : MonoBehaviour
{
    [SerializeField] private GameObject QuestLogUI;
    
    private StarterAssetsInputs starterAssetsInputs;
    
    void Start()
    {
        // Ensure that the StarterAssetsInputs component is on the same GameObject
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        if (starterAssetsInputs == null)
        {
            Debug.LogError("StarterAssetsInputs component not found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (starterAssetsInputs != null)
        {
            if (QuestLogUI.activeSelf)
                starterAssetsInputs.ToggleMenuOnOff(true);
            else
                starterAssetsInputs.ToggleMenuOnOff(false);
        }
    }
}
