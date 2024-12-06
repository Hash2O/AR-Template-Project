using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;

//Methodology (using Unity Input System) :
// - Add ARPlaneManager adn ARRaycastManager to XROrigin
// - Add this script to XROrigin
// - Add the prefab to be placed to the <see cref="placedPrefab"/>
// - Create a new input system named TouchControls with <Pointer>/press as the binding
// Touch screen to place the prefab to be spawned on the touch position
// Note : it will only placed the prefab if the touch position is on detected trackables
// Move the existing spawned object on the touch position

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : PressInputBase
{
    //Prefab to be placed on touch
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at touch position")]
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


    // Update is called once per frame
    void Update()
    {
        //Check if there is any pointer device connected to the system
        //or if touch input occurs
        if(Pointer.current == null || isPressed == false) 
            return;

        //Store current touch position
        var touchPosition = Pointer.current.position.ReadValue();

        //Check if the raycast hits any trackables
        if(raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            //Raycast hits are sorted by distance, the first is the closet
            var hitPose = hits[0].pose;

            if(spawnedObject == null)
            {
                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                //Change the spawned object position and rotation to the touch position
                spawnedObject.transform.position = hitPose.position;
                spawnedObject.transform.rotation = hitPose.rotation;
            }

            //To make the spawned object always look at the camera
            Vector3 lookPos = Camera.main.transform.position - spawnedObject.transform.position;
            lookPos.y = 0f;
            spawnedObject.transform.rotation = Quaternion.LookRotation(lookPos);

        }

    }

    protected override void OnPress(Vector3 position) => isPressed = true;

    protected override void OnPressCancel() => isPressed = false;


}
