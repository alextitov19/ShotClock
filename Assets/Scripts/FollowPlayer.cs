using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public Vector3 rotation;


    private void Start()
    {
        transform.eulerAngles = transform.eulerAngles + rotation;
    }

    void Update()
    {
        transform.rotation = player.rotation;
        transform.position = player.position + offset;
    }
}
