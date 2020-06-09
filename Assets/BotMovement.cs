using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour
{
    public Transform player;
    public int movementSpeed;
    void Update()
    {
        transform.LookAt(player);
        transform.Translate(0, 0, movementSpeed * Time.deltaTime);
        
    }
}
