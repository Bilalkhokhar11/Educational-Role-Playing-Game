using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public Transform followTarget;

    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] public float distance = 3.8f;

    [SerializeField] public float minVerticalAngle = -45;
    [SerializeField] float maxVerticalAngle = 45;

    [SerializeField] Vector2 framingOffset;

    [SerializeField] bool invertX;
    [SerializeField] bool invertY;
    float cameraRadius = 0.3f; // tweak as needed
    [SerializeField] LayerMask collisionMask;


    float rotationX;
    float rotationY;

    float invertXVal;
    float invertYVal;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        invertXVal = (invertX) ? -1 : 1;
        invertYVal = (invertY) ? -1 : 1;

        rotationX += Input.GetAxis("Camera Y") * invertYVal * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        rotationY += Input.GetAxis("Camera X") * invertXVal * rotationSpeed;

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);
        var focusPosition = followTarget.position + new Vector3(framingOffset.x, framingOffset.y);

        // Desired camera position
        Vector3 desiredCameraPosition = focusPosition - targetRotation * new Vector3(0, 0, distance);

        // Raycast or Spherecast to check for obstacles
        RaycastHit hit;
        Vector3 directionToCamera = (desiredCameraPosition - focusPosition).normalized;

        float adjustedDistance = distance;

        if (Physics.SphereCast(focusPosition, cameraRadius, directionToCamera, out hit, distance, collisionMask))
        {
            adjustedDistance = hit.distance - 0.2f;
        }

        // Final camera position
        transform.position = focusPosition - targetRotation * new Vector3(0, 0, adjustedDistance);
        transform.rotation = targetRotation;
    }


    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);
}
