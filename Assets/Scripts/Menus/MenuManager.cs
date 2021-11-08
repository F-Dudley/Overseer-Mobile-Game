using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Objects")]
    public GameObject mainMenu;
    public GameObject settingsMenu;

    [Space]

    public GameObject mainButtonsMenu;
    public GameObject gameChoicesMenu;

    [Space]

    [Header("Main Menu Objects")]
    public Button playButton;
    public Button settingsButton;
    public Button quitButton;

    [Header("Settings Menu Objects")]
    public Button settingsReturnButton;

    #region Unity Functions
    private void Start()
    {
        playButton.onClick.AddListener(PlayButtonClicked);
        settingsButton.onClick.AddListener(SettingsButtonClicked);
        quitButton.onClick.AddListener(QuitButtonClicked);

        LoadEnviromentChoices();
    }

    private void Update()
    {
        
    }
    #endregion

    #region Menu Buttons
    public void PlayButtonClicked()
    {
        OpenGameChoices();
    }

    public void SettingsButtonClicked()
    {

    }

    public void QuitButtonClicked()
    {

    }
    #endregion

    #region PlaySelector
    private void LoadEnviromentChoices()
    {

    }

    #endregion

    #region MenuStates

    public void OpenSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void OpenGameChoices()
    {
        if (mainMenu.activeSelf)
        {
            mainButtonsMenu.SetActive(false);
            gameChoicesMenu.SetActive(true);
        }
    }

    public void CloseGameChoices()
    {
        if (mainMenu.activeSelf)
        {
            mainButtonsMenu.SetActive(true);
            gameChoicesMenu.SetActive(false);
        }
    }

    #endregion
}
