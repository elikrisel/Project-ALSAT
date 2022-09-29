using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditButton : MonoBehaviour
{
    public GameObject menuLogic;
    [SerializeField] GameObject[] players;
  
    // Onclick så visar den credit menyn
    public void MouseClick()
    {
        menuLogic.GetComponent<MenuLogic>().showCredit = true;

    }
    public void hideCredits()
    {
        transform.parent.gameObject.SetActive(false);
        for (int i = 0; i < players.Length; i++) {
            Destroy(players[i]);
        }
    }
}
