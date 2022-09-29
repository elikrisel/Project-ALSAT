using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuLogic : MonoBehaviour
{

    public bool showMainMenu = false;
    
    public GameObject menuHolder;
    public GameObject creditsPanelParent;
    public bool showGuide = false;
    public bool showParent = false;
    public bool showCredit = false;

 
    public GameObject bakgrund;
    public GameObject Characters;
    public GameObject title;
    public GameObject pressAnyKey;

  
  

    // Starta spel, ladda scen 1 
    public void StartGame()
    {
        SceneManager.LoadScene(1);

    }

    public void SelectLevel()
    {
        SceneManager.LoadScene(4);

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    // Asvluta spel 
    public void QuitGame()
    {
        Application.Quit();
    }



    void Update(){

        if (showMainMenu == false)
        { // Gömmer Menuknapparna
            menuHolder.SetActive(false);
        }

        else if (showMainMenu == true && menuHolder.activeSelf == false)
        { // Visar Menuknapparna, stänger andra menyer om de är öppna och vissa objekt
            menuHolder.SetActive(true);
            showGuide = false;
            showCredit = false;
            showParent = false;
            if (bakgrund)
            {
                bakgrund.SetActive(true);
                
            }
        
          
           
          
            //HEJ
        }

       
            
        

        if (showCredit == false)
        { // Om Den är false så gömmer den settings
            creditsPanelParent.SetActive(false);
        }
        else if (showCredit == true && creditsPanelParent.activeSelf == false)
        { //visar credit därför stänger vi av menyknapparna och objekt
            creditsPanelParent.SetActive(true);
            showMainMenu = false;
            showParent = false;
           // bakgrund.SetActive(false);
           // title.SetActive(false);
           

          
           
          
        }

    }
        
    // Fadear ut tryck på valfri knappt för att börja och startar menyn
    public void SpawnMenu()
    {
        StartCoroutine(SpawnMenuEnumerator());
     //   pressAnyKeyTextObject.GetComponent<Animator>().SetBool("Fadeout", true);
       // Characters.GetComponent<Animator>().SetBool("Fadeout", true);
     
    }
    IEnumerator SpawnMenuEnumerator()
    {
        yield return new WaitForSeconds(1);
        if (!showCredit)
        {
            showMainMenu = true;
        }
    
        Destroy(Characters);
        Destroy(bakgrund);
        Destroy(title);
        Destroy(pressAnyKey);






    }
}
