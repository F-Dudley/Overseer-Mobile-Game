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
    private NavMeshSurface navMeshSurface;

    [Header("Game References")]
    public bool gameActive;

    [Header("Game Events")]
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

    #region Unity Functions
    private void Awake()
    {
        instance = this;
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    private void Start() {
        gameEnviroment = Instantiate<GameObject>(gameEnviroment, Vector3.zero, Quaternion.identity);
        gameEnviroment.SetActive(false);
    }

    private void OnEnable() {

    }

    private void Update()
    {

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
