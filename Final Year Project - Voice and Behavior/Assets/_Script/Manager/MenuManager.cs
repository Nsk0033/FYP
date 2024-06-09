using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;

public class MenuManager : MonoBehaviour
{
    [Header("MainMenuPanel")]
    [SerializeField] private GameObject mainMenuCollection;

    [Header("PlayMenuPanel")]
    [SerializeField] private GameObject playMenuCollection;
    [SerializeField] private GameObject saveFileHorizontalLayoutGrp;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject levelHorizontalLayoutGrp;
    [SerializeField] private Button levelButton;


    [Header("SettingMenuPanel")]
    [SerializeField] private GameObject settingMenuCollection;

    [Header("CreditMenuPanel")]
    [SerializeField] private GameObject creditMenuCollection;

    [Header("AudioSetting")]
    [SerializeField] private AudioSource bgm;
    [SerializeField] private Slider volumnSlider;


    private void Start()
    {
        ActivateMenuPanel(mainMenuCollection.name);
        saveFileHorizontalLayoutGrp.SetActive(false);
        levelHorizontalLayoutGrp.SetActive(false);
    }

    private void Update()
    {
        bgm.volume = volumnSlider.value;
    }

    public void OnPlayGameBtnPressed() 
    {
        ActivateMenuPanel(playMenuCollection.name);
        saveFileHorizontalLayoutGrp.SetActive(true);
        playButton.interactable = false;
    }

    public void OnPlayBtnClicked() 
    {
        levelButton.interactable = true;
        playButton.interactable = false;
        saveFileHorizontalLayoutGrp.SetActive(true);
        levelHorizontalLayoutGrp.SetActive(false);
    }

    public void OnLevelBtnClicked()
    {
        levelButton.interactable = false;
        playButton.interactable = true;
        levelHorizontalLayoutGrp.SetActive(true);
        saveFileHorizontalLayoutGrp.SetActive(false);
    }

    public void OnStartBtnClicked(int sceneIndex) 
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void OnSettingBtnPressed()
    {
        ActivateMenuPanel(settingMenuCollection.name);
    }

    public void OnCreditBtnPressed()
    {
        ActivateMenuPanel(creditMenuCollection.name);
    }

    public void OnCloseBtnClicked() 
    {
        ActivateMenuPanel(mainMenuCollection.name);
    }

    public void OnExitBtnPressed() 
    {
        Application.Quit();
    }

    private void ActivateMenuPanel(string menuName)
    {
        mainMenuCollection.SetActive(menuName.Equals(mainMenuCollection.name));
        playMenuCollection.SetActive(menuName.Equals(playMenuCollection.name));
        settingMenuCollection.SetActive(menuName.Equals(settingMenuCollection.name));
        creditMenuCollection.SetActive(menuName.Equals(creditMenuCollection.name));
    }
}
