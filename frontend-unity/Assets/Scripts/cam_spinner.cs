using UnityEngine;

public class cam_spinner : MonoBehaviour
{
    [SerializeField] private float rotation_speed;
    
    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * rotation_speed);
    }
}
