using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//Source URL : https://www.youtube.com/watch?v=7GiDoWviQEM

public class MultiImagesTrackingManager : MonoBehaviour
{

    [SerializeField]
    private GameObject[] prefabsToSpawn;

    private ARTrackedImageManager _arTrackedImageManager;

    private Dictionary<string, GameObject> _arObjects;

    // Initialization of the ARTrackedImageManager
    private void Awake()
    {
        _arTrackedImageManager = GetComponent<ARTrackedImageManager>();
        _arObjects = new Dictionary<string, GameObject>();
    }
    
    private void Start()
    {
        //Listening events related to any change in ARTrackedImageManager
        _arTrackedImageManager.trackedImagesChanged += OnTrackedImageChanged;

        //Spawning GameObjects related to images and hide them
        foreach (GameObject prefab in prefabsToSpawn)
        {
            GameObject newARObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newARObject.name = prefab.name;
            newARObject.gameObject.SetActive(false);
            _arObjects.Add(newARObject.name, newARObject);
        }
    }

    private void OnDestroy()
    {
        _arTrackedImageManager.trackedImagesChanged += OnTrackedImageChanged;
    }

    private void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        //Identify changes on tracked images
        foreach(ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateTrackedImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateTrackedImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(false);
        }
    }

    private void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        //Checking tracked image status
        if(trackedImage.trackingState is TrackingState.Limited or TrackingState.None)
        {
            _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(false);
            return;
        }

        //Show, hide or position GameObject on tracked image
        if(prefabsToSpawn != null)
        {
            _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(true);
            _arObjects[trackedImage.referenceImage.name].transform.position = trackedImage.transform.position;
        }
    }
}
