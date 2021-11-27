using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class OriginManagement : MonoBehaviour
{
    [SerializeField] private ARPlaneManager planeManager;

    #region Unity Functions
    private void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
    }

    private void OnEnable()
    {
        GameManager.EnviromentStartPlacement.AddListener(ShowPlaneVisability);
        GameManager.EnviromentPlaced.AddListener(HidePlaneVisablity);
    }

    private void OnDisable()
    {
        GameManager.EnviromentPlaced.RemoveListener(ShowPlaneVisability);
        GameManager.EnviromentPlaced.RemoveListener(HidePlaneVisablity);
    }
    #endregion

    #region Placement Helpers
    private void ShowPlaneVisability()
    {
        planeManager.requestedDetectionMode = PlaneDetectionMode.Horizontal;

        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(true);
        }
    }

    private void HidePlaneVisablity()
    {
        planeManager.requestedDetectionMode = PlaneDetectionMode.None;

        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
    }
    #endregion
}
