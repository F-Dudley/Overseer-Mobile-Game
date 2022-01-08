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

    [Header("Game Events")]
    public static UnityEvent EnviromentStartPlacement;    
    public static UnityEvent EnviromentPlaced;

    public static UnityEvent GameWaveStarted;
    public static UnityEvent GameWaveEnded;

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
    }

    private void Update()
    {
        if (placementScript.EnviromentPlaced)
        {
            Debug.Log("Now in Game State");
        }
        else
        {
            placementScript.PlacementProcess();
        }
    }
    #endregion
}
