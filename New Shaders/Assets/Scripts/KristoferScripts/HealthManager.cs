using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HealthManager : MonoBehaviour
{
    #region Public Variables
    
    //Health UI array
    [Header("Health/Hearts")]
    [Tooltip("Health Image Game Objects")]
    public GameObject[] life;
    //Total amount of lives. 3 for testing purposes, might have more for final game.
    [Tooltip("Total number of lives the players have combined")]
    public int numberofLives;
    
    #endregion

    
    #region Private Variables
    public bool isDead;
    public GameManager gm;
    #endregion

    #region Main Methods
    void Start()
    {
      numberofLives = life.Length;
      gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        
      
   
            if (isDead == true)
            {

                Debug.Log("Game Over");
            //Adding a upcoming "game over" function from the game manager script
            if (gm)
            {
                gm.Invoke("GameOver", 5f);
            }
            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().death = true;
            GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Movement>().death = true;
            }

             

    }
    #endregion



    

    #region Taking Damage
    
    public void TakeDamage(int decrease)
    {
          //  Debug.Log(numberofLives + "!");
            if (numberofLives >= 1)
            {
            numberofLives -= decrease;
            life[numberofLives].gameObject.GetComponent<Hearts_Flash>().call_reduce_alpha();
            // animation for losing a life in this function

          //  this.gameObject.GetComponent<Animator>().SetBool("Life", false);

            if (numberofLives < 1)
                {

                    isDead = true;

                }


            }

       
        
        



    }
    
    #endregion
    #region Collectible
   
    public void AddHealth()
    {
        Debug.Log("added");
          life[numberofLives].gameObject.SetActive(true);
        life[numberofLives].gameObject.GetComponent<Hearts_Flash>().call_increase_alpha();
        numberofLives += 1;

        Debug.Log(numberofLives);
            // animation for gaining life in this function
    }
    

    
    private void OnTriggerEnter(Collider other)
    {

  

    }
    
    #endregion

}
