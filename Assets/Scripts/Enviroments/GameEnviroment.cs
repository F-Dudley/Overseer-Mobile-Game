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
    public Transform targetLocation;

    [Header("Enemies")]
    public EnviromentEnemy[] lowEnemies;
    public EnviromentEnemy[] mediumEnemies;
    public EnviromentEnemy[] hardEnemies;
}
