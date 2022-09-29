using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sideways : MonoBehaviour
{
    #region Public Variables
    public float rightLimit = 2.5f;
    public float leftLimit = -2.5f;
    public float speed = 2.0f;
    #endregion

    #region Private variables
    private int direction = 1;
    bool moving_dir;
    [SerializeField] float friction_range;
    #endregion

    #region Main Method
    private void Start()
    {
        decide_limits();
        decide_moving_dir();
    }
    #endregion

    #region Moving directions of the platforms
    void decide_limits() {
        rightLimit = transform.position.x + rightLimit;
        leftLimit = transform.position.x + leftLimit;
    }
    void decide_moving_dir() {
        var range = Random.Range(0, 10);
        if (range > 5)
        {
            direction= 1;
        }
        else {
            direction = -1;
        }
    }
    
    void FixedUpdate()
    {
        if (transform.position.x >rightLimit && transform.position.x > leftLimit)
        {
            direction = -1;
        }
        else if (transform.position.x < leftLimit && transform.position.x < rightLimit)
        {
            direction = 1;
        }
        Vector3 movement = Vector3.right * direction * speed * Time.deltaTime;
        GetComponent<Rigidbody>().velocity = movement;
    }
    #endregion
    void OnCollisionStay(Collision collision)
    {
      

        /*   if ( collision.gameObject.CompareTag("Grab")  && !collision.gameObject.GetComponent<Grab_Status>().grabbed)
           {
               for (int i = 0; i < collision.contacts.Length; i++) {
                   if (Vector3.Dot(transform.up,collision.GetContact(i).normal)<friction_range){
                       collision.collider.transform.SetParent(transform);
                   }
               }
           }*/
    }
    void OnCollisionExit(Collision collision)
    {
        
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerTwo"))
        {
            collision.collider.transform.SetParent(null);
        }
        if (collision.gameObject.CompareTag("Grab") ) { 
            collision.collider.transform.SetParent(null);
        }

    }

}
