using System.Collections;
using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using System.IO;

public class PlayerScript : MonoBehaviour
{
    public Vector3 movementVector;
    
    int spawnCounter = 0;
    int amountToSpawn;
    int totalRooms;

    public float fireRate;
    public float weaponRange;
    private float nextFire;
    public float rotationSpeed;

    public GameObject botPrefab;
    public GameObject roomClearedUI;

    public Rigidbody playerBody;
    public Camera myCamera;
    private GameObject door;

    private Room currentRoom;

    XmlDocument roomDataXml;
    XmlNodeList roomNodeList;
    Room[] roomList;

    private void Start()
    {
        Cursor.visible = false;
        InvokeRepeating("SpawnBot", 5.0f, 10.0f);
    }

    private void Awake()
    {
        
        TextAsset xmlTextAsset = Resources.Load<TextAsset>("roomData");
        roomDataXml = new XmlDocument();
        roomDataXml.LoadXml(xmlTextAsset.text);
        LoadRooms();
        currentRoom = roomList[0];
        amountToSpawn = currentRoom.initSpawnAmount;
        door = GameObject.Find(currentRoom.doorName);
        Debug.Log("Successfully loaded " + roomList.Length + " rooms");
    }

    private void LoadRooms()
    {
        roomNodeList = roomDataXml.SelectNodes("/RoomList/Room");
        totalRooms = int.Parse(roomDataXml.SelectSingleNode("RoomList")["TotalRooms"].InnerText);
        roomList = new Room[totalRooms];

        foreach (XmlNode room in roomNodeList)
        {
            roomList[int.Parse(room.Attributes["index"].Value)] = new Room(room);
        }
    }

    void Update()
    {

        if(Global.killCounter == currentRoom.totalMobs && currentRoom.isCleared == false)
        {
            currentRoom.isCleared = true;
            Debug.Log("Entered kC = " + Global.killCounter);
            door.SetActive(false);
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
        if (++spawnCounter == currentRoom.mobWaves + 1) 
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

    private void SetRoom(int i)
    {
        if (roomList[i].isCleared == false)
        {
            CancelInvoke("SpawnBot");
            currentRoom = roomList[i];
            spawnCounter = 0;
            amountToSpawn = currentRoom.initSpawnAmount;
            door = GameObject.Find(currentRoom.doorName);
            InvokeRepeating("SpawnBot", 5.0f, 10.0f);
        } 
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
            SetRoom(doormatRoom - 1);
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
    public string doorName;
    public bool isCleared = false;
    int spAmount;

    public Room(XmlNode curRoom)                 //current room is passed in
    {
        XmlNode spListNode = curRoom.SelectSingleNode("SpawnPointList");  //parent node for all spawnpoints nodes
        spAmount = int.Parse(spListNode["TotalSpawnPoints"].InnerText);
        mobSpawnPoints = new int[spAmount, 2];  //start assigning spawnpoints to a 2d integer array
        int i = 0;
        foreach (XmlNode spawnPointNode in spListNode.SelectNodes("SpawnPoint"))  //itterate through all spawnpoints for the room
        {
            mobSpawnPoints[i, 0] = int.Parse(spawnPointNode["X"].InnerText);
            mobSpawnPoints[i, 1] = int.Parse(spawnPointNode["Z"].InnerText);
            i++;
        }                                       //spawnpoints assigned
        totalMobs = int.Parse(curRoom["TotalMobs"].InnerText);  //totalMobs assigned
        mobWaves = int.Parse(curRoom["MobWaves"].InnerText);  //mobWaves assigned
        doorName = curRoom["DoorName"].InnerText;  //String for door name used to find the object
    }
}