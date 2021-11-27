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
    private int health;
    private int money;

    [HideInInspector]
    public static GameManager instance;
    public ARPlacement placementScript;
    private NavMeshSurface navMeshSurface;

    [Header("Game References")]
    public bool gameActive;

    [Header("Game Events")]
    public static UnityEvent EnviromentStartPlacement;    
    public static UnityEvent EnviromentPlaced;

    public static UnityEvent RoundStart;
    public static UnityEvent RoundFinish;

    private Coroutine gameWavesProcess;

    [Header("Enviroment References")]
    [SerializeField] private static GameObject gameEnviroment;
    [SerializeField] private static GameEnviroment gameEnviromentScript;    

    [Header("Game Enemies")]
    public GameObject basicEnemy;
    public GameObject mediumEnemy;
    public GameObject hardEnemy;

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

    public static Vector3 GameTarget
    {
        get {
            return gameEnviromentScript.targetLocation.position;
        }
    }

    public int Money
    {
        set
        {
            money = value;
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
        gameEnviroment.transform.localScale = gameEnviroment.transform.localScale * 0.2f;
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
