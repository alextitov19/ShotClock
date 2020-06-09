using UnityEngine;

public class BotScript : MonoBehaviour
{
    public Transform player;

    public int movementSpeed;
    void Update()
    {
        transform.LookAt(player);
        transform.Translate(0, 0, movementSpeed * Time.deltaTime);

        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Bot collided with bullet");
        }

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Bot collided with player");
        }
    }
}
