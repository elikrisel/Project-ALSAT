using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle_Trigger : MonoBehaviour
{
    [SerializeField] GameObject bear;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerTwo")) {
            bear.GetComponent<Bear>().rampage = true;
        }
    }
}
