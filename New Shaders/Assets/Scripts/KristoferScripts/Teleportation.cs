using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{

    //Fun script for playing around my scene. Might keep it for final build?

    #region Public Variables
    public Transform teleport;
    public GameObject player;
    #endregion

    #region Teleport on Trigger
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerTwo"))
            player.transform.position = teleport.transform.position;
        

    }
    #endregion


}
