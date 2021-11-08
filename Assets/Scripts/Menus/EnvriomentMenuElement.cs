using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EnvriomentMenuElement : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI flavourText;
    public Image sampleImage;

    private EnvironmentAsset assetToSelect;
    public EnvironmentAsset AssetToSelect
    {
        set {
            assetToSelect = value;
            MapElementValues();
        }
    }

    private void MapElementValues()
    {
        title.text = assetToSelect.environmentName;
        flavourText.text = assetToSelect.environmentDescription;

        if (assetToSelect.environmentPicture != null)
        {
            sampleImage.sprite = assetToSelect.environmentPicture;
        }
    }

    public void LoadARScene()
    {
        GameManager.GameEnviroment = assetToSelect.environmentPrefab;
        SceneManager.LoadScene("ARScene", LoadSceneMode.Single);
    }
}
