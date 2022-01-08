using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Objects")]
    public GameObject mainMenu;
    public GameObject settingsMenu;

    [Space]

    public GameObject mainButtonsMenu;
    public GameObject gameChoicesMenu;

    public GameObject returnButton;

    [Space]

    [Header("Main Menu Objects")]
    public Button playButton;
    public Button settingsButton;
    public Button quitButton;

    [Header("Play Menu Objects")]
    public RectTransform buttonsContainer;

    public GameObject environmentButtonPrefab;

    #region Unity Functions
    private void Start()
    {
        playButton.onClick.AddListener(PlayButtonClicked);
        settingsButton.onClick.AddListener(SettingsButtonClicked);
        quitButton.onClick.AddListener(QuitButtonClicked);

        returnButton.GetComponentInChildren<Button>().onClick.AddListener(ReturnToMainMenu);

        LoadEnvironmentChoices();
    }

    private void Update()
    {
        
    }
    #endregion

    #region Menu Buttons
    public void PlayButtonClicked()
    {
        OpenGameChoices();
        OpenReturnButton();
    }

    public void SettingsButtonClicked()
    {
        OpenSettingsMenu();
        OpenReturnButton();
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }
    #endregion

    #region PlaySelector
    private void LoadEnvironmentChoices()
    {
        EnvironmentAsset[] downloadedEnvironments = Resources.LoadAll<EnvironmentAsset>("GameEnvironments");

        float prefabHeight = environmentButtonPrefab.GetComponent<RectTransform>().sizeDelta.y;
        buttonsContainer.sizeDelta = new Vector2(buttonsContainer.sizeDelta.x, (prefabHeight + 20) * downloadedEnvironments.Length);

        foreach (EnvironmentAsset environment in downloadedEnvironments)
        {
            GameObject buttonElement = Instantiate<GameObject>(environmentButtonPrefab, Vector3.zero, Quaternion.identity);
            buttonElement.transform.SetParent(buttonsContainer);

            buttonElement.transform.localScale = Vector3.one;

            EnvironmentMenuElement buttonElementScript = buttonElement.GetComponent<EnvironmentMenuElement>();
            buttonElementScript.AssetToSelect = environment;
        }
    }

    #endregion

    #region MenuStates

    private void OpenReturnButton()
    {
        returnButton.SetActive(true);
    }

    private void CloseReturnButton()
    {
        returnButton.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);

        mainButtonsMenu.SetActive(true);
        gameChoicesMenu.SetActive(false);

        returnButton.SetActive(false);
    }

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

    #endregion
}
