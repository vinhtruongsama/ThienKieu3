using UnityEngine;
using UnityEngine.InputSystem;

public sealed class Map001CameraRig : MonoBehaviour
{
    public Transform target;
    public float mapWidth = 96f;
    public float mapHeight = 18f;
    public float followLerp = 5f;
    private Camera cam;
    private Vector3 dragOffset;
    private Vector2 lastPointer;
    private bool dragging;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true;
        cam.orthographicSize = mapHeight * 0.25f; // Camera height is half the map height.
    }

    private void LateUpdate()
    {
        var mouse = Mouse.current;
        if (mouse != null)
        {
            if (mouse.leftButton.wasPressedThisFrame)
            {
                dragging = true;
                lastPointer = mouse.position.ReadValue();
            }
            if (mouse.leftButton.wasReleasedThisFrame) dragging = false;
            if (dragging && mouse.leftButton.isPressed)
            {
                Vector2 now = mouse.position.ReadValue();
                Vector3 before = cam.ScreenToWorldPoint(new Vector3(lastPointer.x, lastPointer.y, -transform.position.z));
                Vector3 after = cam.ScreenToWorldPoint(new Vector3(now.x, now.y, -transform.position.z));
                dragOffset += before - after;
                lastPointer = now;
            }
        }

        if (target == null) return;
        Vector3 desired = new Vector3(target.position.x, target.position.y + 2.5f, transform.position.z) + dragOffset;
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;
        desired.x = Mathf.Clamp(desired.x, halfWidth, mapWidth - halfWidth);
        desired.y = Mathf.Clamp(desired.y, halfHeight, mapHeight - halfHeight);
        transform.position = Vector3.Lerp(transform.position, desired, Time.deltaTime * followLerp);
    }
}
