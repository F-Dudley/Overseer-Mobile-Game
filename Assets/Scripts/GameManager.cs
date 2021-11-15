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

    [Header("Game Enemies")]
    public GameObject basicEnemy;

    [Header("Game Events")]
    private Coroutine gameWavesProcess;

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

    #region Unity Functions
    private void Awake()
    {
        instance = this;
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    private void Start() {
        Instantiate(gameEnviroment, new Vector3(0, 0, 50), Quaternion.identity);
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
        gameActive = true;

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
