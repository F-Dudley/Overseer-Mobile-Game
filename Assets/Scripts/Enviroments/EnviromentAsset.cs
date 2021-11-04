using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Game_Enviroment", menuName = "ScriptableObjects/GameEnviroment", order = 1)]
public class EnviromentAsset : ScriptableObject
{
    public string envriomentName;
    public string envriomentDescription;
    public Sprite enviromentPicture;

    [Space]

    public GameObject envriomentPrefab;
}
