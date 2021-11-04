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
    }

    private void Update()
    {

    }
    #endregion

    #region Menu Buttons
    public void PlayButtonClicked()
    {
        SceneManager.LoadScene("ARScene", LoadSceneMode.Single);
    }

    public void SettingsButtonClicked()
    {

    }

    public void QuitButtonClicked()
    {

    }
    #endregion
}
