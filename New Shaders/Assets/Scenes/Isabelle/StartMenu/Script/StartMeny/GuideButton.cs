using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideButton : MonoBehaviour
{

    public GameObject menuLogic;

    // Vid mouse enter så startar animator IsHovering
    public void MouseEnter()
    {

        this.gameObject.GetComponent<Animator>().SetBool("IsHovering", true);
    }

    // Vid mouse exit så sätts Ishovering till false
    public void MouseExit()
    {
        this.gameObject.GetComponent<Animator>().SetBool("IsHovering", false);
    }
   
    
    // Onclick så visar den guide menyn
    public void MouseClick()
    {
        menuLogic.GetComponent<MenuLogic>().showGuide = true;

    }
}


