using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainAnimation : MonoBehaviour
{

    public GameObject terrainArea;
    public GameObject baseArea;
    public GameObject enviromentObjects;

    public float animationTime;

    void Start()
    {
        terrainArea.transform.localScale = Vector3.zero;
        baseArea.transform.localScale = Vector3.zero;
        enviromentObjects.transform.localScale = Vector3.zero;

        LeanTween.scale(terrainArea, Vector3.one, animationTime);
        LeanTween.scale(baseArea, Vector3.one, animationTime);
        LeanTween.scale(enviromentObjects, Vector3.one, animationTime);
    }

    void Update()
    {
        
    }
}
