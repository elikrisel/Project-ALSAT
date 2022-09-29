using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newGame : MonoBehaviour{

    // Vid mouse enter så startar animator IsHovering
    public void MouseEnter(){

        this.gameObject.GetComponent<Animator>().SetBool("IsHovering", true);
    }

    // Vid mouse exit så sätts Ishovering till false

    public void MouseExit() {
        this.gameObject.GetComponent<Animator>().SetBool("IsHovering", false);
    }

}
