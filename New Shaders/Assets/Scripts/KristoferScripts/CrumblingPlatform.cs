using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingPlatform : MonoBehaviour
{
    #region Components
    [SerializeField]
   GameObject platform;
    [SerializeField]
    GameObject manager;

    public Rigidbody rb;
    #endregion

    #region Public Variables that can be changed in Inspector
    [Range(1, 10)]
    public float fallTimer;
    [Range(1, 10)]
    public float respawnTimer;
    #endregion

    #region Main Method
    void Start()
    {

        rb = GetComponent<Rigidbody>();


    }
    #endregion
    #region OnCollision Method
    // Calling from the Platform Manager script. When one of the players hits the crumbling platform, it's going to call the function with the gameobject
    // and the two public variables that sets the timers of both when the platform is going to fall and when it's going to respawn.
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerTwo"))
        {
            manager.GetComponent<PlatformManager>().CallRespawn(this.gameObject,fallTimer,respawnTimer);
            manager.GetComponent<PlatformManager>().CancelInvoke("cancel_functions");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerTwo"))
        {
            manager.GetComponent<PlatformManager>().Invoke("cancel_functions",3);
        }
    }
    #endregion



}
