using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    private bool gamePaused;
    [SerializeField]GameObject pausePanel;
    [SerializeField] GameObject pausedFirstButton;
    [SerializeField]EventSystem firstSelect;
    public GameObject[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        gamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gamePaused && hinput.anyGamepad.start.justPressed)
        {
            PausedGame();
            gamePaused = true;
        }
        else if(gamePaused && hinput.anyGamepad.start.justPressed)
        {
            ContinueGame();
            gamePaused = false;
        }
    }
    void PausedGame()
    {
        
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        firstSelect.SetSelectedGameObject(buttons[0]);
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}
