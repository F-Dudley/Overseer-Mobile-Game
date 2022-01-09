using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnviromentEnemy
{
    public string name;
    public GameObject enemyPrefab;
}

public class GameEnviroment : MonoBehaviour
{
    [Header("Enviroment Points")]
    public Transform spawnPoint;

    [Space]

    public Transform assaultTarget;
    public Transform artilleryTarget;
    public Transform supportTarget;

    [Header("Enviroment Containers")]
    public Transform enemyContainer;

    [Header("Enemies")]
    public EnviromentEnemy[] assultEnemies;
    public EnviromentEnemy[] artilleryEnemies;
    public EnviromentEnemy[] supportEnemies;

    #region Gameplay Functions
    public GameObject GetRandomAssaultEnemy() => assultEnemies[Random.Range(0, assultEnemies.Length - 1)].enemyPrefab;
    public GameObject GetRandomArtilleryEnemy() => artilleryEnemies[Random.Range(0, artilleryEnemies.Length - 1)].enemyPrefab;
    public GameObject GetRandomSupportEnemy() => supportEnemies[Random.Range(0, supportEnemies.Length - 1)].enemyPrefab;
    #endregion
}