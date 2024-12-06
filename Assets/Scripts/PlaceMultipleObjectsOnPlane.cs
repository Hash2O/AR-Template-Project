using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceMultipleObjectsOnPlane : PressInputBase
{
    //Prefab to be placed on touch
    [SerializeField]
    [Tooltip("Instantiates this prefab on the plane at touch position")]
    private GameObject placedPrefab;

    //Instantiated object
    GameObject spawnedObject;

    //Check if touch input occurs
    bool isPressed;

    ARRaycastManager raycastManager;
    List<ARRaycastHit> hits = new();


    protected override void Awake()
    {
        base.Awake();
        raycastManager = GetComponent<ARRaycastManager>();
    }

    protected override void OnPress(Vector3 position)
    {
        if (raycastManager.Raycast(position, hits, TrackableType.PlaneWithinPolygon))
        {
            //Raycast hits are sorted by distance, the first is the closet
            var hitPose = hits[0].pose;

            spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);

            //To make the spawned object always look at the camera
            Vector3 lookPos = Camera.main.transform.position - spawnedObject.transform.position;
            lookPos.y = 0f;
            spawnedObject.transform.rotation = Quaternion.LookRotation(lookPos);
        }
    }
}
