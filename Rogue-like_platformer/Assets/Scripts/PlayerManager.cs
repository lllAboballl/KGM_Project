using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    void Awake()
    {
        //exitRoomText.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("RoomExit"))
        {
            //exitRoomText.SetActive(true);

            if (Input.GetButtonDown("Interact"))
            {

            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("RoomExit"))
        {
           // exitRoomText.SetActive(false);
        }
    }
}
