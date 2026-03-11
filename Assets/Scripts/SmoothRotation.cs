using UnityEngine;

public class SmoothRotation : MonoBehaviour
{
    [Tooltip("Degrees per second around Y-axis")]
    public float rotationSpeed = 30f;

    private bool rotating = true;

    void Update()
    {
        if (!rotating) return;

        // Rotate around world Y axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }

    public void StopRotation()
    {
        rotating = false;
    }

    public void StartRotation()
    {
        rotating = true;
    }
}