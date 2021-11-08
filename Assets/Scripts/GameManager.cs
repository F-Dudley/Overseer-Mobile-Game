using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Unity.AI.Navigation;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Main Variables")]
    public int score;

    private static GameObject gameEnviroment;

    [HideInInspector]
    public static GameManager instance;
    private NavMeshSurface navMeshSurface;

    [Header("Game References")]
    public bool gameActive;
    private Transform spawnPosition;
    private Transform targetPosition;

    [Space]

    public TextMeshProUGUI scoreText;

    [Header("Game Enemies")]
    public GameObject basicEnemy;

    [Header("Game Events")]

    private Coroutine gameWavesProcess;

    #region Unity Functions
    private void Awake()
    {
        instance = this;
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    private void Start() {

    }

    private void OnEnable() {

    }

    private void Update()
    {

    }
    #endregion

    #region Game Functions

    public static GameObject GameEnviroment {
        set { gameEnviroment = value; }
    }

    public void InitializeScene() {
        ResetScore();
        gameActive = true;
    }

    public void UpdateNavMesh() {
        
    }

    private void AddScore() {
        score += 1;
        scoreText.text = "Score: " + score;
    }

    private void ResetScore() {
        score = 0;
        scoreText.text = "Score: " + score;
    }

    public void EndScene() {
        gameActive = false;
        StopCoroutine(gameWavesProcess);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length > 0) {
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }            
        }

    }

    IEnumerator GameWaves() {
        while (gameActive) {
            yield return new WaitForSeconds(3f);

            for (int i = 0; i < 10; i++)
            {
                GameObject newEnemy = Instantiate(basicEnemy, spawnPosition.position, spawnPosition.rotation);
                newEnemy.GetComponent<Enemy>().target = targetPosition;

                yield return new WaitForSeconds(1f);                     
            }
        }
    }

    #endregion
}
