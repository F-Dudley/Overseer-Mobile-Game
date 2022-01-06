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
}