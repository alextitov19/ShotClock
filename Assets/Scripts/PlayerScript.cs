using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Vector3 movementVector;
    
    int spawnCounter = 0;
    int amountToSpawn;

    public float fireRate;
    public float weaponRange;
    private float nextFire;
    public float rotationSpeed;

    public GameObject botPrefab;
    public GameObject roomClearedUI;

    public Rigidbody playerBody;
    public Camera myCamera;

    private Room currentRoom;

    private void Start()
    {
        Cursor.visible = false;
        InvokeRepeating("SpawnBot", 5.0f, 10.0f);
        Room room1 = new Room();
        room1.door = GameObject.Find("Door1");
        room1.totalMobs = 15;
        room1.mobWaves = 5;
        room1.initSpawnAmount = 1;
        room1.mobSpawnPoints = new int[,] {{18, 22}, {18, -22}, {-18, 22}, {-18, -22}};

        amountToSpawn = room1.initSpawnAmount;

        currentRoom = room1;
    }

    void Update()
    {

        if(Global.killCounter == currentRoom.totalMobs && currentRoom.isCleared == false)
        {
            currentRoom.isCleared = true;
            Debug.Log("Entered kC = " + Global.killCounter);
            currentRoom.door.SetActive(false);
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
        if (++spawnCounter == currentRoom.mobWaves) 
        {
            CancelInvoke("SpawnBot");
        }

        for (int i = 1; i <= amountToSpawn; i++)
        {
            int randomPos = Random.Range(0, currentRoom.mobSpawnPoints.GetLength(0));
            Debug.Log("Random number generated = " + randomPos);
            Instantiate(botPrefab, new Vector3(currentRoom.mobSpawnPoints[randomPos, 0], 1, currentRoom.mobSpawnPoints[randomPos, 1]), Quaternion.identity);
        }
        amountToSpawn++;
    }
        

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.tag == "Bot")
        {
    //        Debug.Log("Player collided with bot");
        }
        if (collidedObject.tag == "Doormat") 
        {
            int doormatRoom = collidedObject.name[7] - '0';
            Debug.Log("--Collided with Doormat, current room = " + doormatRoom);
        }
    }

    IEnumerator RoomCleared()
    {
        roomClearedUI.SetActive(true);
        yield return new WaitForSeconds(3);
        roomClearedUI.SetActive(false);
    }

}

class Room
{
    public int[,] mobSpawnPoints;
    public int totalMobs, mobWaves, initSpawnAmount;
    public GameObject door;
    public bool isCleared = false;
}