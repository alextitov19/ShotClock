using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public Vector3 rotationSpeed;

    void Update()
    {
        
        transform.position = player.position + offset;
       transform.Rotate(-Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed.x, Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed.y, 0);
        float z = transform.eulerAngles.z;
        transform.Rotate(0, 0, -z);
    }
}
