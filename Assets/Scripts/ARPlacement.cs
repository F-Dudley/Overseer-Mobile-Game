using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Experimental.XR;

public enum PlacementType
{
    IDLE,
    INITIAL,
    REPLACEMENT
}

public class ARPlacement : MonoBehaviour
{
    public PlacementType currentPlacementType = PlacementType.INITIAL;
    public bool validPlacement = false;

    [Header("Enviroment References")]
    public Transform placementIndicator;
    [SerializeField] private ARSessionOrigin sessionOrigin;
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private Pose placementPose;

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
    }

    public void PlacementProcess(ref Transform placementObject)
    {
        switch (currentPlacementType)
        {
            default:
            case PlacementType.IDLE:
                break;
            
            case PlacementType.INITIAL:
                VisualizePlacement(ref placementIndicator);
                if (validPlacement && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
                {
                    SelectPlacementArea(ref placementObject);
                }
                break;
            
            case PlacementType.REPLACEMENT:
                VisualizePlacement(ref placementObject);
                break;
        }
    }
    #endregion

    #region Placement Functions
    private void VisualizePlacement(ref Transform objectTransform)
    {
        Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();

        raycastManager.Raycast(screenCenter, raycastHits, TrackableType.Planes);
        validPlacement = raycastHits.Count > 0;

        if (validPlacement)
        {
            placementPose = raycastHits[0].pose;
            placementPose.rotation = Quaternion.LookRotation(Camera.current.transform.forward).normalized;
            placementPose.rotation.y = 0;
        }

        UpdateIndicator(ref objectTransform);
    }

    private void UpdateIndicator(ref Transform _objectTransform)
    {
        if (validPlacement)
        {
            _objectTransform.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        _objectTransform.gameObject.SetActive(validPlacement);
    }

    private void SelectPlacementArea(ref Transform _placementObject)
    {
        currentPlacementType = PlacementType.IDLE;
        if (!_placementObject.gameObject.activeSelf) _placementObject.GetComponent<TerrainAnimation>().PlayAnimation();
    }
    #endregion

    #region Object Manipulation Functions

    #endregion
}
