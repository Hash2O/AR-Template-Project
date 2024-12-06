using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneManager : MonoBehaviour
{
    public ARPlaneManager planeManager;

    // Désactive tous les plans et désactive le ARPlaneManager
    public void DisablePlanes()
    {
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
        planeManager.enabled = false;
    }

    // Active tous les plans et active le ARPlaneManager
    public void EnablePlanes()
    {
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(true);
        }
        planeManager.enabled = true;
    }

    // Bascule entre activation et désactivation
    public void TogglePlanes()
    {
        bool isCurrentlyEnabled = planeManager.enabled;

        if (isCurrentlyEnabled)
        {
            DisablePlanes();
        }
        else
        {
            EnablePlanes();
        }
    }
}
