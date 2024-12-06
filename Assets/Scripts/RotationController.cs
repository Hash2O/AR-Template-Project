using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    public GameObject rotatingGO;
    public Vector3 ObjectAxis;

    // Update is called once per frame
    void Update()
    {
        rotatingGO.transform.Rotate(ObjectAxis * Time.deltaTime);
    }
}
