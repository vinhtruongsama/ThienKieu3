using UnityEngine;

public sealed class Map001Parallax : MonoBehaviour
{
    public Transform cameraTransform;
    [Range(0f, 1f)] public float factor = 0.2f;
    private Vector3 origin;
    private float cameraOriginX;

    private void Start()
    {
        origin = transform.position;
        if (cameraTransform != null) cameraOriginX = cameraTransform.position.x;
    }

    private void LateUpdate()
    {
        if (cameraTransform == null) return;
        Vector3 p = origin;
        p.x += (cameraTransform.position.x - cameraOriginX) * factor;
        transform.position = p;
    }
}
