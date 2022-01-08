using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{

    [Header("Round Variables")]
    private Coroutine gameRound;
    private bool roundActive;

    [Header("Game References")]
    [SerializeField] private GameEnviroment gameEnviromentScript;
    [SerializeField] private Camera sceneCamera;

    #region Unity Functions
    private void Start()
    {
        gameEnviromentScript = GameManager.GameEnviroment.GetComponent<GameEnviroment>();
        sceneCamera = Camera.main;
    }

    private void Update()
    {
        if (roundActive)
        {
            PlayerShoot();
        }
    }
    #endregion

    #region Wave Functions
    public void StartGameRound()
    {
        GameManager.GameWaveStarted.Invoke();
        gameRound = StartCoroutine(GameRound());
    }

    private IEnumerator GameRound()
    {
        roundActive = true;

        for (int i = 0; i < 5; i++)
        {
            Instantiate(gameEnviromentScript.GetRandomAssultEnemy(), gameEnviromentScript.spawnPoint.position, gameEnviromentScript.spawnPoint.rotation);
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(10f);

        GameManager.GameWaveEnded.Invoke();
        roundActive = false;
    }
    #endregion

    #region Player Interaction Functionality
    private void PlayerShoot()
    {
        
    }
    #endregion
}
