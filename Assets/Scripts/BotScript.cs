using UnityEngine;

public class BotScript : MonoBehaviour
{
    static GameObject player;
    Transform playerTransform;
    public int movementSpeed;


    private void Start()
    {
        player = GameObject.Find("Player");
        playerTransform = player.transform;
  //      transform.eulerAngles = new Vector3(-90f, 0, 0);
    }
    void Update()
    {
        playerTransform = player.transform;
        transform.LookAt(playerTransform);
        transform.Translate(0, 0, movementSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Bot collided with bullet");
            gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Bot collided with player");
        }
    }
}
