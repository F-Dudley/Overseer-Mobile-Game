using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;

public class TerrainAnimation : MonoBehaviour
{
    [Header("Animation References")]
    private bool animationComplete = false;

    [Header("Main Components")]
    public Transform terrainArea;
    public Transform enviromentObjects;

    [Range(1, 10)]
    public float animationTime;

    public bool isComplete {
        get {
            return animationComplete;
        }
    }

    #region Unity Functions
    private void Awake()
    {
        SetupAnimation();
    }
    #endregion

    #region Main Animation
    private void SetupAnimation()
    {
        gameObject.SetActive(false);

        foreach (Transform child in enviromentObjects)
        {
            child.localScale = Vector3.zero;
        }
    }

    public void PlayAnimation()
    {
        gameObject.SetActive(true);

        Vector3 initialEviromentScale = transform.localScale;
        this.gameObject.transform.localScale = Vector3.zero;

        transform.DOScale(initialEviromentScale, animationTime)
        .SetEase(Ease.InOutCubic)
        .OnComplete(() => {

            foreach (Transform enviromentArea in enviromentObjects)
            {
                enviromentArea.DOScale(Vector3.one, animationTime)
                .SetEase(Ease.OutBack);
            }

            animationComplete = true;
        });
    }
    #endregion
}