using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereGemTrigger : MonoBehaviour
{

    public Transform sphereGemPosition;


    void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("Grab"))
        {

            other.gameObject.transform.position = sphereGemPosition.transform.position;

        }

    }

}
