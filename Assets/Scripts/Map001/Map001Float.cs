using UnityEngine;

public sealed class Map001Float : MonoBehaviour
{
    public float height = 0.16f;
    public float speed = 1.55f;
    private float baseY;
    private void OnEnable() { baseY = transform.position.y; }
    private void Update()
    {
        Vector3 p = transform.position;
        p.y = baseY + Mathf.Sin(Time.time * speed) * height;
        transform.position = p;
    }
}
