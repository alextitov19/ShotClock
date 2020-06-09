using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Rigidbody playerBody;
    public float bulletSpeed;
    public float bulletOffset;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed primary button.");
            transform.position = new Vector3(playerBody.transform.position.x, 1, playerBody.transform.position.z + bulletOffset);
            transform.eulerAngles = new Vector3(0, playerBody.transform.eulerAngles.y, 0);
            InvokeRepeating("MoveBullet", 0.0f, 0.01f);
        }
    }

    void MoveBullet()
    {
        transform.Translate(0, 0, bulletSpeed * Time.deltaTime);
    }
}
