﻿using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    // The target we are following
    [SerializeField]
    private Transform target;

    // The distance in the x-z plane to the target
    [SerializeField]
    private float distance = 10.0f;
    // The height we want the camera to be above the target
    [SerializeField]
    private float height = 5.0f;

    // private float yCamPos = 3.0f;

    [SerializeField]
    private float rotationDamping = 0.0f;

    [SerializeField]
    private float heightDamping = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Early out if we don't have a target
        if(!target)
        {
            return;
        }

        // Calculate the current rotation angles
        var wantedRotationAngle = target.eulerAngles.y;
        var wantedHeight = target.position.y + height;

        var currentRotationAngle = transform.eulerAngles.y;
        var currentHeight = transform.position.y;

        // Dmap the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Dmap the height
        currentRotationAngle = Mathf.Lerp(currentHeight, wantedHeight, heightDamping);

        // Convert the angle into a rotation
        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set position of the camera on the x-z plane to :
        // distance meters behind the target
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        // Set the height of the camera
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // Always look at the target
        transform.LookAt(target);
    }
}
