using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] GameObject door;
    bool opened;
    private void Start()
    {
        opened = false;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if ((collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("PlayerTwo") || collision.gameObject.CompareTag("Grab")) && !opened) {
            Debug.Log("collided");
            Invoke("open_door",1);
            CancelInvoke("close_door");
        }
    } 
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") ||collision.gameObject.CompareTag("PlayerTwo") )
        {
            Debug.Log("uncollided");
            CancelInvoke("open_door");
            Invoke("close_door", 1);
        }
    }
    void close_door() {
        opened = false;
        door.GetComponent<Door>().open = false;
    }
    void open_door() {
        opened = true;
        door.GetComponent<Door>().open = true;
    }
  
}
