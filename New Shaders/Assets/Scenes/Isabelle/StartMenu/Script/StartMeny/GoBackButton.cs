using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoBackButton : MonoBehaviour
{
    public GameObject menuLogic;

    public void GoBack()
    {

        // Close panel 
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);

    }
   
}
