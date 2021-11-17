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
    [Header("Main Variables")]
    public int score;

    [HideInInspector]
    public static GameManager instance;
    public ARPlacement placementScript;
    private NavMeshSurface navMeshSurface;

    [Header("Game References")]
    private static bool enviromentInitialized;
    public bool gameActive;

    [Header("Game Events")]
    public static UnityEvent EnviromentPlaced;
    public static UnityEvent EnviromentReplaced;

    [Space]

    public static UnityEvent RoundStart;
    public static UnityEvent RoundFinish;
    public static UnityEvent RoundLost;

    private Coroutine gameWavesProcess;

    [Header("Enviroment References")]
    [SerializeField] private static GameObject gameEnviroment;
    [SerializeField] private static GameEnviroment gameEnviromentScript;    

    [Header("Game Enemies")]
    public GameObject basicEnemy;
    public GameObject mediumEnemy;
    public GameObject hardEnemy;

    public static bool EnviromentInitialized
    {
        get {
            return enviromentInitialized;
        }
        set {
            enviromentInitialized = value;
            if (value)  {
                EnviromentPlaced.Invoke();
            }
        }
    }

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

    #region Unity Functions
    private void Awake()
    {
        instance = this;
        placementScript = GetComponent<ARPlacement>();
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    private void Start() {
        gameEnviroment = Instantiate<GameObject>(gameEnviroment, Vector3.zero, Quaternion.identity);
        gameEnviroment.SetActive(false);
    }

    private void Update()
    {
        if (enviromentInitialized)
        {
            Debug.Log("Now in Game State");
        }
        else
        {
            placementScript.PlacementProcess(ref gameEnviroment);
        }
    }
    #endregion

    #region Game Functions

    #region Main Functions
    public void InitializeScene() {
        gameActive = false;

    }

    IEnumerator GameWaves() {
        yield return null;
    }    

    public void EndScene() {

    }
    #endregion

    #region Sub Functions

    #endregion

    #endregion
}
