using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
    private int selectedLevel;
    private int levelIndex;
    [SerializeField] TextMeshProUGUI levelDisplay;
    // Start is called before the first frame update
    private void Start()
    {   
        if(SceneManager.GetActiveScene().name == "JohanTestScene" && levelIndex == 0)
        {
            levelIndex = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("Levels", levelIndex);
        }
        else if(levelIndex > SceneManager.GetActiveScene().buildIndex)
        {
            levelIndex = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("Levels", levelIndex);
        }
        Debug.Log("Current SceneIndex: " + SceneManager.GetActiveScene().buildIndex + " - " + "levelIndex = " + levelIndex);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    public void SelectLevel(int i)
    {
        returnSelectedLevel(i);
       // levelDisplay.text = "Selected Level: Level " + (selectedLevel - 1);
        Debug.Log("Selected Level: " + selectedLevel);
    }
    public int returnSelectedLevel(int i)
    {
        selectedLevel = i;
        return selectedLevel;
    }
    public void LoadLevel() 
    {
        SceneManager.LoadScene(selectedLevel);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
