using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    //Simple checkpoint script. Once one of the players hits the checkpoint trigger gameobject, they'll spawn at that location once they die.
    //Also calling the Game Manager script for it

    #region Checkpoint trigger
    void OnTriggerEnter(Collider other)
    {
     
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerTwo"))
        {

            GameManager.Instance.lastCheckpoint = transform;

        }


    }
    #endregion

}
