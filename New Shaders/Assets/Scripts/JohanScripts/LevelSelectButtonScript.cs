using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButtonScript : MonoBehaviour
{

    public Button[] levelbuttons;
    // Start is called before the first frame update
    void Start()
    {
        
        int levelReached = PlayerPrefs.GetInt("Levels", 3);
        for(int i = 0; i < levelbuttons.Length; i++)
        {
            if(i + 1 > levelReached)
            {
                levelbuttons[i].interactable = false;
            }
        }
    }

}
