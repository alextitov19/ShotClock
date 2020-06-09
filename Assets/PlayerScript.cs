using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody playerBody;
    public Vector3 movementVector;
    public float rotationSpeed;
  
        
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

}
