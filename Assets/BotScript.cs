using UnityEngine;

public class BotScript : MonoBehaviour
{
    public Transform player;
    public Rigidbody bullet;
    public int movementSpeed;
    void Update()
    {
        transform.LookAt(player);
        transform.Translate(0, 0, movementSpeed * Time.deltaTime);
        
    }
}
