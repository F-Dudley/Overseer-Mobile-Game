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
    public float animationTimeMin;
    [Range(1, 10)]
    public float animationTimeMax;

    public List<Material> materials = new List<Material>();

    public bool isComplete {
        get {
            return animationComplete;
        }
    }

    #region Unity Functions
    private void Start()
    {
        if (animationTimeMin >= animationTimeMax) animationTimeMin--;

        Vector3 initialEviromentScale = transform.localScale;

        SetupAnimation();
        StartAnimation(initialEviromentScale);
    }
    #endregion

    private void GetChildMaterials()
    {
        Renderer[] childMaterials = transform.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in childMaterials)
        {
            foreach (Material mat in renderer.sharedMaterials)
            {
                if (!materials.Contains(mat)) materials.Add(mat);
            }
        }
    }

    public void ChangeAlpha(float _alpha)
    {
        foreach (Material mat in materials)
        {
            mat.DOFade(_alpha, 1);
        }
    }

    private void SetupAnimation()
    {
        this.gameObject.transform.localScale = Vector3.zero;

        foreach (Transform child in enviromentObjects)
        {
            child.localScale = Vector3.zero;
        }

        GetChildMaterials();
    }

    private void StartAnimation(Vector3 _initialEnviromentScale)
    {
        transform.DOScale(_initialEnviromentScale, animationTimeMin)
        .SetEase(Ease.OutQuad)
        .OnComplete(() => {

            foreach (Transform enviromentArea in enviromentObjects)
            {
                enviromentArea.DOScale(Vector3.one, Random.Range(animationTimeMin, animationTimeMax)).SetEase(Ease.OutBack);
            }

            animationComplete = true;
            ChangeAlpha(0.5f);
        });
    }
}
