using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Unity.AI.Navigation;
using TMPro;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager instance;

    public ARPlacement placementScript;
    private NavMeshSurface navMeshSurface;

    public static event UnityAction EnviromentStartPlacement;    
    public static event UnityAction EnviromentPlaced;

    public static event UnityAction GameWaveStarted;
    public static event UnityAction GameWaveEnded;

    [Header("Enviroment References")]
    [SerializeField] private static GameObject gameEnviroment;
    [SerializeField] private static GameEnviroment gameEnviromentScript;

    public static GameObject GameEnviroment
    {
        get { 
            return gameEnviroment; 
        }
        set {
            gameEnviroment = value; 
            gameEnviromentScript = value.GetComponent<GameEnviroment>();
        }
    }

    public static GameEnviroment GameEnviromentScript
    {
        get {
            return gameEnviromentScript;
        }
    }

    public static Transform SpawnPoint
    {
        get {
            return gameEnviromentScript.spawnPoint;
        }
    }

    public static Transform EnemyContainer
    {
        get {
            return gameEnviromentScript.enemyContainer;
        }
    }

    public static Vector3 AssaultTarget
    {
        get {
            return gameEnviromentScript.assaultTarget.position;
        }
    }

    public static Vector3 ArtilleryTarget
    {
        get {
            return gameEnviromentScript.artilleryTarget.position;
        }
    }

    public static Vector3 SupportTarget
    {
        get {
            return gameEnviromentScript.supportTarget.position;
        }
    }

    #region Unity Functions
    private void Awake()
    {
        instance = this;
        placementScript = GetComponent<ARPlacement>();
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    private void Start()
    {
        gameEnviroment = Instantiate<GameObject>(gameEnviroment, Vector3.zero, Quaternion.identity);
        gameEnviroment.transform.localScale *= 0.05f;
        gameEnviroment.SetActive(false);

        EnviromentPlaced += EnviromentPlacedTest;
    }

    private void Update()
    {
        if (!placementScript.EnviromentPlaced)
        {
            placementScript.PlacementProcess();
        }
    }
    #endregion

    #region Events
    private void EnviromentPlacedTest()
    {
        Debug.Log("Enviroment Placed Invoked");
    }

    public static void InvokeEnviromentStartedPlacement() => EnviromentStartPlacement.Invoke();
    public static void InvokeEnviromentPlaced() => EnviromentPlaced.Invoke();
    public static void InvokeGameWaveStarted() => GameWaveStarted.Invoke();
    public static void InvokeGameWaveEnded() => GameWaveEnded.Invoke();
    #endregion
}
