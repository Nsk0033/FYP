using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUI : MonoBehaviour
{
    [SerializeField] private GameObject Map;
	
	private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onMapPressed += MapPressed;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onMapPressed -= MapPressed;
    }

    private void MapPressed()
    {
        if (Map.activeInHierarchy)
        {
            HideUI();
        }
        else
        {
            ShowUI();
        }
    }
	
	private void ShowUI()
    {
        Map.SetActive(true);
		Time.timeScale = 0f;
    }

    public void HideUI()
    {
        Map.SetActive(false);
		Time.timeScale = 1f;
    }
}
