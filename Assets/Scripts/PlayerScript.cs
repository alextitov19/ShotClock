using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody playerBody;
    public Vector3 movementVector;
    public float rotationSpeed;
    public GameObject botPrefab;
    int spawnCounter = 0;
    int amountToSpawn = 1;
    public Camera myCamera;
    public float fireRate;
    public float weaponRange;
    private float nextFire;



    private void Start()
    {
        Cursor.visible = false;
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

        if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
        {
            Debug.Log("Pressed primary button.");
            nextFire = Time.time + fireRate;
            Vector3 rayOrigin = myCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, myCamera.transform.forward, out hit, weaponRange))
            {
                hit.rigidbody.gameObject.SetActive(false);
            }

        }

        if (Input.GetKey(KeyCode.Escape))
            Screen.lockCursor = false;
        else
            Screen.lockCursor = true;

    }

    private void SpawnBot()
    {
        if (++spawnCounter == 5) CancelInvoke("SpawnBot");
        for (int i = 1; i <= amountToSpawn; i++)
        {
            Instantiate(botPrefab, new Vector3(Random.Range(18.0f, 23.0f) * RandomSign(), 1, Random.Range(18.0f, 23.0f) * RandomSign()), Quaternion.identity);
        }
        amountToSpawn++;
    }
        

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bot")
        {
            Debug.Log("Player collided with bot");
        }
    }

    private int RandomSign()
    {
        if (Random.Range(0, 2) == 0)
        {
            return -1;
        }
        return 1;
    }

}
