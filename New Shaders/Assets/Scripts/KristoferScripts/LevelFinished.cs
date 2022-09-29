using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinished : MonoBehaviour
{
    //This script was meant for the testing purposes, but going to either use it as a final version or take parts of it for SceneManager script.
    
    public GameManager theEnd;
    public int number;

    #region On finished level
    // When both players hits a trigger at the end of the level, it's going to call the function from the Game Manager.Both players must be in the trigger
    // otherwise they can't complete the level.
    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerTwo"))
        {
            number++;
            Debug.Log(number);

        }
       if(number == 2)
        {

            theEnd.OnLevelComplete();

        }


                    
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerTwo"))
        number--;
        Debug.Log(number);
    }
    #endregion
}
