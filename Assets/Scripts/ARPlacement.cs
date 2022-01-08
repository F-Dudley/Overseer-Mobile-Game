using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Experimental.XR;

public enum PlacementType
{
    INITIAL,
    REPLACEMENT,
    PLACED
}

public class ARPlacement : MonoBehaviour
{
    [Header("Main Placement Variables")]
    [SerializeField] private PlacementType currentPlacementType = PlacementType.INITIAL;
    private bool enviromentPlaced = false;
    private bool validPlacement = false;

    [Header("Scene References")]
    public Camera mainCam;
    public Transform placementIndicator;
    [SerializeField] private ARSessionOrigin sessionOrigin;
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private Pose placementPose;

    public bool EnviromentPlaced
    {
        set {
            enviromentPlaced = value;
            if (value)
            {
                GameManager.InvokeEnviromentPlaced();
            }
        }
        get {
            return enviromentPlaced;
        }
    }

    public PlacementType CurrentPlacementType
    {
        set {
            currentPlacementType = value;
        }
    }

    #region Main Functions
    private void Start()
    {
        sessionOrigin = FindObjectOfType<ARSessionOrigin>();
        raycastManager = FindObjectOfType<ARRaycastManager>();

        if (sessionOrigin == null || raycastManager == null)
        {
            Debug.Log("Cannot Find Neccassary Components.");
        }

        currentPlacementType = PlacementType.INITIAL;
        GameManager.InvokeEnviromentStartedPlacement();
    }

    public void PlacementProcess()
    {
        switch (currentPlacementType)
        {
            default:
            case PlacementType.PLACED:
                break;
            
            case PlacementType.INITIAL:
                VisualizePlacement(placementIndicator);
                break;
            
            case PlacementType.REPLACEMENT:
                VisualizePlacement(GameManager.GameEnviroment.transform);
                break;
        }
    }
    #endregion

    #region Placement Functions
    private void VisualizePlacement(Transform objectTransform)
    {
        Vector3 screenCenter = mainCam.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();

        raycastManager.Raycast(screenCenter, raycastHits, TrackableType.Planes);
        validPlacement = raycastHits.Count > 0;

        if (validPlacement)
        {
            placementPose.position = raycastHits[0].pose.position;
            placementPose.rotation = Quaternion.LookRotation(new Vector3(mainCam.transform.forward.x, 0, mainCam.transform.forward.z).normalized);
        }

        UpdateIndicator(objectTransform);
    }

    private void UpdateIndicator(Transform _objectTransform)
    {
        if (validPlacement)
        {
            _objectTransform.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        _objectTransform.gameObject.SetActive(validPlacement);
    }

    public void SelectPlacementArea()
    {
        GameManager.GameEnviroment.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        if (!GameManager.GameEnviroment.activeSelf && currentPlacementType == PlacementType.INITIAL)
        {
            GameManager.GameEnviroment.GetComponent<TerrainAnimation>().PlayAnimation();        
        }
        EnviromentPlaced = true;
        currentPlacementType = PlacementType.PLACED;
    }
    #endregion

    #region Object Manipulation Functions

    #endregion
}
