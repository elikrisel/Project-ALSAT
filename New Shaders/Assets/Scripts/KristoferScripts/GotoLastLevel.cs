using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoLastLevel : MonoBehaviour
{

    public GameManager gm;
    public int numberofPlayers;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerTwo"))
        {
            numberofPlayers++;
            Debug.Log(numberofPlayers);

        }
        if (numberofPlayers == 2)
        {

            gm.MoveToLastLevel();

        }



    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerTwo"))
            numberofPlayers--;
        Debug.Log(numberofPlayers);
    }
}