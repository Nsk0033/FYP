using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShow : MonoBehaviour
{
	[SerializeField] private List<GameObject> icons;

    private void Update()
    {
        // Check if all game objects in the list are inactive
        bool allInactive = true;
        foreach (var icon in icons)
        {
            if (icon.activeSelf)
            {
                allInactive = false;
                break;
            }
        }

        // If all are inactive, call the ShowNSK method
        if (allInactive)
        {
            ShowNSK();
        }
    }

    public void ShowNSK()
    {
        ShowNPCManager.instance.ShowNPC();
    }
}
