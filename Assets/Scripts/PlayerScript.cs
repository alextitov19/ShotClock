using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Vector3 movementVector;
    
    int spawnCounter = 0;
    int amountToSpawn = 1;

    public float fireRate;
    public float weaponRange;
    private float nextFire;
    public float rotationSpeed;

    private bool roomIsCleared = false;

    public GameObject door1;
    public GameObject botPrefab;
    public GameObject roomClearedUI;

    public Rigidbody playerBody;
    public Camera myCamera;

    private void Start()
    {
        Cursor.visible = false;
        InvokeRepeating("SpawnBot", 5.0f, 10.0f);
    }

    void Update()
    {

        if(Global.killCounter == 15 && roomIsCleared == false)
        {
            roomIsCleared = true;
            Debug.Log("Entered kC = " + Global.killCounter);
            door1.SetActive(false);
            StartCoroutine(RoomCleared());
        }

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
                GameObject hitObject = hit.rigidbody.gameObject;
                if (hitObject.tag == "Bot")
                {
                    Debug.Log("Bot killed");
                    hitObject.SetActive(false);
                    Global.killCounter++;
                    Debug.Log("Kill counter = " + Global.killCounter);
                }
            }

        }

        if (Input.GetKey(KeyCode.Escape))
            Screen.lockCursor = false;
        else
            Screen.lockCursor = true;

    }

    private void SpawnBot()
    {
        if (++spawnCounter == 5) 
        {
            CancelInvoke("SpawnBot");
        }

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
    //        Debug.Log("Player collided with bot");
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

    IEnumerator RoomCleared()
    {
        roomClearedUI.SetActive(true);
        yield return new WaitForSeconds(3);
        roomClearedUI.SetActive(false);
    }

}
