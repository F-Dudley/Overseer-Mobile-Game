using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Game_Enviroment", menuName = "ScriptableObjects/GameEnviroment", order = 1)]
public class EnvironmentAsset : ScriptableObject
{
    public string environmentName;
    public string environmentDescription;
    public Sprite environmentPicture;

    [Space]

    public GameObject environmentPrefab;
}
