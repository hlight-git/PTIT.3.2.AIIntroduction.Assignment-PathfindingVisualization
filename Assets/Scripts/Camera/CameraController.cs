using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform cameraTransform;
    [SerializeReference] private Quaternion newRotation;
    [SerializeReference] private Vector3 newZoom;
    [SerializeReference] private Vector3 rotateStartPosition;
    [SerializeReference] private Vector3 rotateCurrentPosition;
    public Vector3 diff;
    void Start()
    {
        cameraTransform = GetComponentInChildren<Transform>();
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }
    void Update()
    {
        HandleMouseInput();
    }
    void HandleMouseInput()
    {
        if (OnMouse.hoverABlock && Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * transform.forward;
        }
        if (Input.GetMouseButtonDown(2))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            rotateCurrentPosition = Input.mousePosition;
            diff = rotateCurrentPosition - rotateStartPosition;
            rotateStartPosition = rotateCurrentPosition;
            if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
                newRotation *= Quaternion.Euler(Vector3.up * (diff.x / 5f));
            else
                newRotation *= Quaternion.Euler(Vector3.left * (diff.y / 5f));
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * 2);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * 2);
    }
}
