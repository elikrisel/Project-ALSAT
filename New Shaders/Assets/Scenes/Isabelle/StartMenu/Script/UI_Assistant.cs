using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI_Assistant : MonoBehaviour{

    private TextMeshPro messageText;


    private void Awake()  {
        messageText = transform.Find("message").Find("messageText").GetComponent<TMPro.TextMeshPro>();
        Application.targetFrameRate = 3;
    }

    private void Start(){

        Debug.Log("hej");
     
    }
}
