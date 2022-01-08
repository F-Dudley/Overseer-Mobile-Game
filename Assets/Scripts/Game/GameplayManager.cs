using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{

    [Header("Round Variables")]
    private Coroutine gameRound;
    private bool roundActive;

    [Header("Player Shooting")]
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private bool shootingReady;
    [SerializeField] private int shootDamage = 100;

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
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) PlayerShoot();
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
        if (shootingReady)
        {
            shootingReady = false;
            Vector3 screenCenter = sceneCamera.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));

            if (Physics.Raycast(screenCenter, sceneCamera.transform.forward, out RaycastHit hitInfo, 200f, playerMask, QueryTriggerInteraction.Collide))
            {
                if (hitInfo.collider.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    enemy.TakeDamage(shootDamage);
                }
            }

            StartCoroutine(ShootCoolDown());
        }
    }

    private IEnumerator ShootCoolDown()
    {
        yield return new WaitForSeconds(5f);
        shootingReady = true;
    }
    #endregion
}
