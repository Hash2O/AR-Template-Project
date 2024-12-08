using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARObjectsInteractionManager : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_RaycastManager;

    private List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();

    private Camera arCam;

    private GameObject spawnedObject;

    [SerializeField]
    private GameObject[] spawnableObjects;

    private AudioSource m_Audiosource;

    // Start is called before the first frame update
    void Start()
    {
        spawnedObject = null;
        arCam = Camera.main;
        m_Audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //If nothing, do .. nothing
        if (Input.touchCount == 0) { return; }

        //If touch occurs
        Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);

        if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && spawnedObject == null)
            {

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.gameObject.CompareTag("ARObject"))
                    {
                        //hit.collider.gameObject.GetComponent<Canvas>().enabled = false;
                        SpawnPrefab(m_Hits[0].pose.position);
                    }
                    else
                    {
                        spawnedObject = hit.collider.gameObject;
                    }
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved && spawnedObject != null)
            {
                spawnedObject.transform.position = m_Hits[0].pose.position;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                spawnedObject = null;
            }
        }

    }

    private void SpawnPrefab(Vector3 spawnPosition)
    {
        spawnedObject = Instantiate(spawnableObjects[Random.Range(0, spawnableObjects.Length)], spawnPosition, Quaternion.identity);
        m_Audiosource.Play();
    }
}
