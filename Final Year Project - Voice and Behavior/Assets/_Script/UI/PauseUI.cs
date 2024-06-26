using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
	
	private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onPausePressed += PausePressed;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onPausePressed -= PausePressed;
    }

    private void PausePressed()
    {
        if (PauseMenu.activeInHierarchy)
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
        PauseMenu.SetActive(true);
		Time.timeScale = 0f;
    }

    public void HideUI()
    {
        PauseMenu.SetActive(false);
		Time.timeScale = 1f;
    }
	
	public void SaveGame()
	{
		Debug.Log("Save");
	}
	
	public void LoadMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
	
}
