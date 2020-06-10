using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody playerBody;
    public Vector3 movementVector;
    public float rotationSpeed;
    public GameObject botPrefab;
    int counter = 0;



    private void Start()
    {
        InvokeRepeating("SpawnBot", 5.0f, 10.0f);
    }

    void Update()
    {
        playerBody.transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed, 0);

        if (Input.GetKey("d"))
        {
            playerBody.transform.Translate(movementVector.x * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("a"))
        {
            playerBody.transform.Translate(-movementVector.x * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("w"))
        {
            playerBody.transform.Translate(0, 0, movementVector.z * Time.deltaTime);
        }

        if (Input.GetKey("s"))
        {
            playerBody.transform.Translate(0, 0, -movementVector.z * Time.deltaTime);
        }

    }

    private void SpawnBot()
    {
        if (++counter == 5) CancelInvoke("SpawnBot");
        Instantiate(botPrefab, new Vector3(0, 1.1f, 0), Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bot")
        {
            Debug.Log("Player collided with bot");
        }
    }

}
