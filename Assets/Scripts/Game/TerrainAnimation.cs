using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainAnimation : MonoBehaviour
{
    [Header("Animation References")]
    private bool animationComplete = false;

    [Header("Main Components")]
    public GameObject terrainArea;
    public GameObject enviromentObjects;

    public float animationTime;

    void Start()
    {
        Vector3 initialEviromentScale = transform.localScale;

        SetupAnimation();
        AnimationStart(initialEviromentScale);
    }

    public bool AnimationComplete {
        get {
            return animationComplete;
        }
    }

    private void SetupAnimation()
    {
        this.gameObject.transform.localScale = Vector3.zero;
    }

    private void AnimationStart(Vector3 _initialEnviromentScale)
    {
        LeanTween.scale(this.gameObject, _initialEnviromentScale, animationTime)
        .setEaseOutCubic()
        .setOnComplete(() => {
            animationComplete = true;
        });
    }
}
