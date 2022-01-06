using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMenuManager : MonoBehaviour
{
    // Singleton Reference
    public static GameMenuManager instance;

    [Header("Game Functions")]
    [SerializeField] private Image healthImage;
    [SerializeField] private Image shieldImage;
    [SerializeField] private TextMeshProUGUI resourcesText;

    [Header("Menus")]
    [SerializeField] private GameObject placementUI;
    [SerializeField] private GameObject gameUI;

    #region Unity Functions
    private void OnEnable()
    {
        GameManager.EnviromentPlaced.AddListener(OpenGameUI);
        GameManager.EnviromentStartPlacement.AddListener(OpenPlacementUI);
    }

    private void OnDisable()
    {
        GameManager.EnviromentPlaced.RemoveListener(OpenGameUI);
        GameManager.EnviromentStartPlacement.RemoveListener(OpenPlacementUI);
    }
    #endregion

    #region UI Change Functions
    public void SetHealthUI(float _currentHealth, float _maxHealth)
    {
        healthImage.fillAmount = _currentHealth / _maxHealth;
    }

    public void SetShieldUI(float _currentShield, float _maxShield)
    {
        shieldImage.fillAmount = _currentShield / _maxShield;
    }

    public void SetResourcesUI(int _resourcesAmount)
    {
        resourcesText.text = string.Format("Resources: {0}", _resourcesAmount);
    }

    public void OpenPlacementUI()
    {
        placementUI.SetActive(true);
        gameUI.SetActive(false);
    }

    public void OpenGameUI()
    {
        gameUI.SetActive(true);
        placementUI.SetActive(false);
    }
    #endregion
}
