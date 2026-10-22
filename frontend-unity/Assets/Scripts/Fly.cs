using UnityEngine;

public class Fly : MonoBehaviour
{
    [SerializeField] private Transform body;
    [SerializeField] private float rotationSpeed = 10f;

    private Vector3 previousPosition;

    private void Start()
    {
        if (body == null) body = transform;
        previousPosition = transform.position;
    }

    private void Update()
    {
        // 1. Calculate velocity/direction vector based on movement this frame
        Vector3 movement = transform.position - previousPosition;

        // 2. Project onto the XZ plane to isolate Y-axis rotation
        movement.y = 0f;

        // 3. Rotate towards direction if moving (prevents snapping back to zero when idle)
        if (movement.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement.normalized, Vector3.up);
            body.rotation = Quaternion.Slerp(body.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        previousPosition = transform.position;
    }
}
