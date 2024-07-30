using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Transform mainCameraTransform;

    void Start()
    {
        // Find the MainCamera object and get its transform
        GameObject mainCameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        if (mainCameraObject != null)
        {
            mainCameraTransform = mainCameraObject.transform;
        }
        else
        {
            Debug.LogWarning("MainCamera not found. Ensure the MainCamera is tagged as 'MainCamera'.");
        }
    }

    void Update()
    {
        if (mainCameraTransform != null)
        {
            // Rotate towards the main camera
            transform.LookAt(mainCameraTransform);
        }
    }
}
