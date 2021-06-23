using System.Collections;
using System.Collections.Generic;
using MiscUtil.Xml.Linq.Extensions;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementScirpt : MonoBehaviour
{
    public GameObject _spawnObj;
    // public GameObject _ball;
    private GameObject _ballSpawned;
    public GameObject _placementIndicator;

    public static GameObject spawnedObject;

    private Pose _placementPose;
    private ARRaycastManager _arRaycastManager;
    private bool _placementPoseIsValid = false;
    // Start is called before the first frame update
    void Start()
    {
        _arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(spawnedObject == null && _placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //     ARPlaceObject();
        if (_placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            ARPlaceObject();
        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    void UpdatePlacementIndicator()
    {
        if(spawnedObject == null && _placementPoseIsValid)
        {
            _placementIndicator.SetActive(true);
            _placementIndicator.transform.SetPositionAndRotation(_placementPose.position, _placementPose.rotation);
        }
        else
            _placementIndicator.SetActive(false);
    }

    void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();

        _arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        _placementPoseIsValid = hits.Count > 0;
        if(_placementPoseIsValid)
        {
            _placementPose = hits[0].pose;
        }

    }

    void ARPlaceObject()
    {
        // var ballPos = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0f));
        // _ballSpawned = Instantiate(_ball);
        // _ballSpawned.transform.parent = _arRaycastManager.transform.Find("AR Camera").gameObject.transform;
        _spawnObj.SetActive(true);
        spawnedObject = _spawnObj;
        // spawnedObject = Instantiate(_spawnObj, _placementPose.position, _placementPose.rotation);
        // _ball.SetActive(true);
    }

}
