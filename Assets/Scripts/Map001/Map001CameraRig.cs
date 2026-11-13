using UnityEngine;
using UnityEngine.InputSystem;

public sealed class Map001CameraRig : MonoBehaviour
{
    public Transform target;

    [Header("Ranh giới bản đồ — tọa độ thế giới")]
    [Tooltip("Mép trái của toàn vùng camera được phép nhìn.")]
    public float mapMinX = 0f;
    [Tooltip("Mép phải của toàn vùng camera được phép nhìn.")]
    public float mapMaxX = 96f;
    [Tooltip("Mép dưới của toàn vùng camera được phép nhìn.")]
    public float mapMinY = 0f;
    [Tooltip("Mép trên của toàn vùng camera được phép nhìn.")]
    public float mapMaxY = 24f;

    [Header("Khung nhìn camera")]
    [Tooltip("Orthographic Size. Tăng để thu nhỏ hình và nhìn được nhiều bản đồ hơn.")]
    [Min(0.1f)]
    public float cameraSize = 6f;
    [Tooltip("Độ cao tâm camera so với người chơi. Đặt 0 để lấy người chơi làm tâm theo chiều dọc.")]
    public float verticalOffset = 2.5f;
    [Tooltip("Tốc độ camera tiến tới vị trí theo người chơi.")]
    [Min(0f)]
    public float followLerp = 5f;

    private Camera cam;
    private Vector3 dragOffset;
    private Vector2 lastPointer;
    private bool dragging;

    private void OnValidate()
    {
        cameraSize = Mathf.Max(0.1f, cameraSize);
        followLerp = Mathf.Max(0f, followLerp);

        Camera previewCamera = GetComponent<Camera>();
        if (previewCamera == null) return;
        previewCamera.orthographic = true;
        previewCamera.orthographicSize = cameraSize;
    }

    private void Awake()
    {
        cam = GetComponent<Camera>();
        if (cam == null) return;
        cam.orthographic = true;
        cam.orthographicSize = cameraSize;
    }

    private void LateUpdate()
    {
        if (cam == null) cam = GetComponent<Camera>();
        if (cam == null) return;

        var mouse = Mouse.current;
        if (mouse != null)
        {
            if (mouse.leftButton.wasPressedThisFrame)
            {
                dragging = true;
                lastPointer = mouse.position.ReadValue();
            }
            if (mouse.leftButton.wasReleasedThisFrame)
            {
                dragging = false;
                dragOffset = Vector3.zero;
            }
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
        Vector3 desired = new Vector3(target.position.x, target.position.y + verticalOffset, transform.position.z) + dragOffset;
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;
        desired.x = ClampCameraCenter(desired.x, mapMinX, mapMaxX, halfWidth);
        desired.y = ClampCameraCenter(desired.y, mapMinY, mapMaxY, halfHeight);
        transform.position = Vector3.Lerp(transform.position, desired, Time.deltaTime * followLerp);
    }

    private static float ClampCameraCenter(float position, float minEdge, float maxEdge, float halfView)
    {
        float minCenter = minEdge + halfView;
        float maxCenter = maxEdge - halfView;

        // If the camera view is larger than the map on this axis, center it on that axis.
        if (minCenter > maxCenter)
            return (minEdge + maxEdge) * 0.5f;

        return Mathf.Clamp(position, minCenter, maxCenter);
    }
}
