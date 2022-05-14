using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInRoomCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            gameObject.GetComponentInParent<RoomManager>().playerInRoom = true;
            other.gameObject.GetComponent<PlayerStats>().HealToFull();
            Destroy(gameObject);
        }
    }
}
