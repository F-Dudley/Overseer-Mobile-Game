using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{

    [Header("Round Variables")]
    private Coroutine gameRound;
    private WaitForSeconds wait;

    [Header("Game References")]
    [SerializeField] private GameManager gameManager;

    public void StartGameRound()
    {
        gameRound = StartCoroutine(StartRound());
    }

    private IEnumerator StartRound()
    {
        GameManager.RoundStart.Invoke();

        yield return new WaitForSeconds(5);

        GameManager.RoundFinish.Invoke();
    }
}
