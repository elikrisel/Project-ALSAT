using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    #region Text gameobject
    public GameObject text;
    #endregion
    #region Variables
    bool isColliding;
    #endregion

    #region Main Methods
    private void Start()
    {
        text.SetActive(false);
        

    }
    private void Update()
    {
        if(isColliding && hinput.anyGamepad.B.justPressed)
        {
            //If one of the players are inside the trigger and hits the B button on gamepad, it unpauses the game and destroys
            // the text- and trigger gameobject.
            Time.timeScale = 1;
            Destroy(text);
            Destroy(gameObject);
        }
    }
    #endregion

    #region On trigger events
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerTwo"))
        {
            //Pauses the game and setting the text UI active when a player hits the trigger
            Time.timeScale = 0f;
            text.SetActive(true);

            
        }

    }
    private void OnTriggerStay(Collider other)
    {
        isColliding = true;
      


    }
    #endregion




}
