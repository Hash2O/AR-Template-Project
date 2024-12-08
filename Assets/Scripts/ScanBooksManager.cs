using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ScanBooksManager : MonoBehaviour
{
    public TextMeshProUGUI scanInfoText;

    [SerializeField]
    MultiImagesTrackingManager _multiImagesTrackingManager;

    private void Awake()
    {
        _multiImagesTrackingManager = FindObjectOfType<XROrigin>().GetComponent<MultiImagesTrackingManager>();
        scanInfoText.text = "";
    }

    public void EnableScanBook()
    {
        _multiImagesTrackingManager.enabled = true;
        scanInfoText.text = "Scan Enabled";
    }

    public void DisableScanBook()
    {
        _multiImagesTrackingManager.enabled = false;
        scanInfoText.text = "Scan Disabled";
    }

    public void ToggleScan()
    {
        bool isCurrentlyEnabled = _multiImagesTrackingManager.enabled;

        if (isCurrentlyEnabled)
        {
            DisableScanBook();
        }
        else
        {
            EnableScanBook();
        }
    }

}
