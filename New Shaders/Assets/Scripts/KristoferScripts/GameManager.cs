using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    [Header("Start position game object in here")]
    [Tooltip("Tracks current position of the map and checkpoints")]
    public Transform lastCheckpoint;
    [Header("Health UI")]
    //[SerializeField] GameObject HealthUI;
    [SerializeField] int next_level;
    
    [SerializeField] bool gameHasEnded = false;
    [SerializeField] bool movetoLastLevel = false;
    [SerializeField] bool lastLevelComplete = false;

    //This variable was made for testing purposes for the whitebox level. 
    [Header("Cube with the Level finished script + is Trigger")]
    public GameObject levelfinished;

    void Awake()
    {
        //Calling the static function on awake. It ties up with the respawn and checkpoint functions.
        Instance = this;
        

    }
    void Start()
    {
        //DontDestroyOnLoad(HealthUI);

    }

    public void OnLevelComplete()
    {
        //I had this function for testing purposes. I'm keeping this function for now in case we need game manager instead of the scene manager script


        levelfinished.SetActive(true);
        
        Debug.Log("Level finished, loading next scene.");
        //Loading the scene in build settings
        SceneManager.LoadScene(2);


    }
    public void GameOver() // When the players have zero lives left it's going to show this function in the Health manager script
    {
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            
            Debug.Log("Game Over");
            // Setting the game active scene true
            SceneManager.LoadScene(5); //Diegame scene 


        }


    }
    public void MoveToLastLevel()
    {

        if(movetoLastLevel == false)
        {

            movetoLastLevel = true;
            Debug.Log("Going to the last level");
            SceneManager.LoadScene(3);

        }


    }
    public void LastLevelComplete()
    {
        if(lastLevelComplete == false)
        {

            lastLevelComplete = true;
            Debug.Log("Last Level completed");
            SceneManager.LoadScene(6); //Loading the win scene

        }


    }

  
}
