using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;

    [Header("Gameplay Variables")]
    private int baseHealth = 20;
    [SerializeField] private int maxBaseHealth = 20;
    private int shieldHealth = 100;
    [SerializeField] private int maxShieldHealth = 20;

    [SerializeField] private int resourcesAmount = 0;

    [SerializeField] private int currentRound = 1;

    public int BaseHealth
    {
        get {
            return baseHealth;
        }
        set {
            baseHealth = value;
            baseHealth = Mathf.Clamp(baseHealth, 0, maxBaseHealth);
            GameMenuManager.instance.SetHealthUI(baseHealth, maxBaseHealth);
        }
    }

    private int MaxBaseHealth
    {
        set {
            maxBaseHealth = value;
            GameMenuManager.instance.SetHealthUI(baseHealth, maxBaseHealth);
        }
    }

    public int ShieldHealth
    {
        get {
            return shieldHealth;
        }
        set {
            shieldHealth = value;
            shieldHealth = Mathf.Clamp(shieldHealth, 0, maxShieldHealth);
            GameMenuManager.instance.SetShieldUI(shieldHealth, maxShieldHealth);
        }
    }

    public int MaxShieldHealth
    {
        set {
            maxShieldHealth = value;
            GameMenuManager.instance.SetShieldUI(shieldHealth, maxShieldHealth);
        }
    }

    public int ResourcesAmount
    {
        get {
            return resourcesAmount;
        }

        set {
            resourcesAmount = value;
            resourcesAmount = Mathf.Clamp(resourcesAmount, 0, int.MaxValue);
            GameMenuManager.instance.SetResourcesUI(resourcesAmount);    
        }
    }

    [Header("Round Variables")]
    private Coroutine gameRound;
    public bool roundActive;
    [SerializeField] private int activeEnemyAmount = 0;

    [Header("Object Pool")]
    [SerializeField] private int maxPoolSize = 100;

    [SerializeField] public ObjectPool enemyPool;

    [Header("Player Shooting")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private bool shootingReady;
    [SerializeField] private int shootDamage = 100;

    private WaitForSeconds shootingCooldown = new WaitForSeconds(3f);

    [Header("Game References")]
    [SerializeField] private Camera sceneCamera;

    [Header("Wave Helpers")]
    private WaitForSeconds spawnOverlapWaiter = new WaitForSeconds(2f);
    private WaitForSeconds spawnSubWaveWaiter = new WaitForSeconds(3f);

    #region Unity Functions
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
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
        GameManager.InvokeGameWaveStarted();
        gameRound = StartCoroutine(GameRound());
    }

    private IEnumerator GameRound()
    {
        roundActive = true;
        int roundEnemyNumber = 10 * currentRound;

        while (roundEnemyNumber > 0)
        {
            for (int i = 0; i < 5; i++)
            {
                /*
                float randomNum = Random.Range(0, 100);
                if (currentRound >= 20)
                {
                    if (randomNum > 60)
                    {
                        SpawnArtilleryUnit();
                    }
                    else if (randomNum > 30)
                    {
                        SpawnSupportUnit();
                    }
                    else SpawnAssaultUnit();
                }
                else if (currentRound >= 10)
                {
                    if (randomNum > 90)
                    {
                        SpawnArtilleryUnit();
                    }
                    else if (randomNum > 70)
                    {
                        SpawnSupportUnit();
                    }
                    else
                    {
                        SpawnAssaultUnit();
                    }
                }
                else if (currentRound >= 5)
                {
                    if (randomNum > 80)
                    {
                        SpawnSupportUnit();
                    }
                    else
                    {
                        SpawnAssaultUnit();
                    }
                }
                else SpawnAssaultUnit();
                */
                SpawnSupportUnit();

                roundEnemyNumber--;

                yield return spawnOverlapWaiter;
            }
            
            yield return spawnSubWaveWaiter;
        }

        while (activeEnemyAmount > 0)
        {
            yield return null;
        }

        GameManager.InvokeGameWaveEnded();
        roundActive = false;
        currentRound++;
    }

    public void ActiveEnemyDied() => activeEnemyAmount--;
    #endregion

    #region Player Interaction Functionality
    private void PlayerShoot()
    {
        if (shootingReady)
        {
            shootingReady = false;
            Vector3 screenCenter = sceneCamera.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));

            if (Physics.Raycast(screenCenter, sceneCamera.transform.forward, out RaycastHit hitInfo, 200f, enemyMask, QueryTriggerInteraction.Collide))
            {
                if (hitInfo.collider.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    Debug.Log("Hit Enemy");
                    enemy.TakeDamage(shootDamage);
                }
            }

            StartCoroutine(ShootCoolDown());
        }
    }

    private IEnumerator ShootCoolDown()
    {
        yield return shootingCooldown;
        shootingReady = true;
    }

    private void SpawnAssaultUnit()
    {
        GameObject spawnedEnemy = enemyPool.TakeAssaultItem();

        if (spawnedEnemy != null)
        {
            spawnedEnemy.GetComponent<NavMeshAgent>().SetDestination(GameManager.AssaultTarget);            
            activeEnemyAmount++;
        }
    }

    private void SpawnArtilleryUnit()
    {
        GameObject spawnedEnemy = enemyPool.TakeArtilleryItem();
        if (spawnedEnemy != null)
        {
        spawnedEnemy.GetComponent<NavMeshAgent>().SetDestination(GameManager.ArtilleryTarget);            
            activeEnemyAmount++;
        }
    }

    private void SpawnSupportUnit()
    {
        GameObject spawnedEnemy = enemyPool.TakeSupportItem();
        spawnedEnemy.GetComponent<NavMeshAgent>().SetDestination(GameManager.SupportTarget);
    }
    #endregion

    #region Unit Base Functionality
    public void DamageBase(int _damageAmount)
    {
        if (shieldHealth > 0)
        {
            shieldHealth -= _damageAmount;
        }
        else
        {
            baseHealth -= _damageAmount;
        }
    }
    #endregion
}
