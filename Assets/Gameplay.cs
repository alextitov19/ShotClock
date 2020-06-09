using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public Rigidbody playerBody;
    public Rigidbody bulletBody;
    public Vector3 movementVector;
    public float rotationSpeed;
    public float bulletSpeed;
        
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

        

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed primary button.");
            bulletBody.transform.position = new Vector3(playerBody.transform.position.x, 1, playerBody.transform.position.z);
            bulletBody.transform.eulerAngles = new Vector3(0, playerBody.transform.eulerAngles.y, 0);
            InvokeRepeating("moveBullet", 0.0f, 0.01f);
        }


    }

    void moveBullet()
    {
        bulletBody.transform.Translate(0, 0, bulletSpeed * Time.deltaTime);
    }
}
