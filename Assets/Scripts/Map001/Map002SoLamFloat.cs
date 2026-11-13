using UnityEngine;

public sealed class Map002SoLamFloat : MonoBehaviour
{
    public float amplitude = 0.08f;
    public float frequency = 2.2f;
    private Vector3 startPosition;

    private void Start() { startPosition = transform.localPosition; }

    private void Update()
    {
        transform.localPosition = startPosition + Vector3.up * Mathf.Sin(Time.time * frequency) * amplitude;
    }
}
