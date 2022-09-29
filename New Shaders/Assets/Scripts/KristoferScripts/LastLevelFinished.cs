using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastLevelFinished : MonoBehaviour
{
    public GameManager gm;
    public int numberofPlayers;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerTwo"))
        {

            numberofPlayers++;
            Debug.Log(numberofPlayers);


        }
        if(numberofPlayers == 2)
        {

            gm.LastLevelComplete();

        }

    }
    void OnTriggerExit(Collider other)
    {

        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerTwo"))
        {

            numberofPlayers--;
            Debug.Log(numberofPlayers);

        }


    }



}
