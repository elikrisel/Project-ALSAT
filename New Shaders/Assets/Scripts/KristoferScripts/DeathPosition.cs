using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPosition : MonoBehaviour
{
    #region Starting position variable
    [Header("Start Position")]
    [Tooltip("Drag the StartPosition Game Object")]
    public Transform startingPosition;
    
    #endregion
    #region Health Manager script
    public HealthManager health;
    
    #endregion

    #region Main Method
    void Start()
    {
        health = FindObjectOfType<HealthManager>();
        

    }
    
    #endregion

    #region Lose a life when hitting the ground underneath the map
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerTwo"))
        {

            //Calling the function from health manager script to take a life from the players
            // Also calling the Game Manager function that connects with Death Position for respawn and checkpoint purposes
            other.gameObject.transform.position = GameManager.Instance.lastCheckpoint.position;
            health.TakeDamage(1);
            Debug.Log("Losing a life");
        }



    }
    #endregion
}
