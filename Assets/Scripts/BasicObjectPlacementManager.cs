using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class BasicObjectPlacementManager : MonoBehaviour
{
    public GameObject spawnableObject;
    public XROrigin XROrigin;
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;

    private List<ARRaycastHit> hits = new();

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                bool hasHit = raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon);

                if (hasHit && isButtonPressed() == false)
                {
                    GameObject _newObject = Instantiate(spawnableObject);
                    _newObject.transform.position = hits[0].pose.position;
                    _newObject.transform.rotation = hits[0].pose.rotation;
                }

                foreach (var plane in planeManager.trackables)
                {
                    plane.gameObject.SetActive(false);
                }

                planeManager.enabled = false;
            }
        }
    }

    public bool isButtonPressed()
    {
        if (EventSystem.current.currentSelectedGameObject?.GetComponent<Button>() == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void SwitchObject(GameObject _selectedObject)
    {
        spawnableObject = _selectedObject;
    }
}
