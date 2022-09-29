using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] Transform portal;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Grab"))
        {
            portal.gameObject.SetActive(true);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grab"))
        {
            portal.gameObject.SetActive(true);
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Grab"))
        {
            portal.gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Grab"))
        {
            portal.gameObject.SetActive(false);
        }
    }
}
