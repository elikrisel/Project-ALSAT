using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineField : MonoBehaviour
{
    private bool isBallOne;
    private bool isBallTwo;
    [SerializeField] Transform portal;
    [SerializeField] Transform ballOne;
    [SerializeField] Transform ballTwo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ball2")
        {
            isBallTwo = true;
            Debug.Log("ball2");
        }
        if (other.gameObject.name == "Ball1")
        {
            isBallOne = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Ball1")
        {
            isBallOne = true;
        }

        if (other.gameObject.name == "Ball2")
        {
            isBallTwo = true;
        }

    }

    private void Update()
    {

        if (isBallOne && isBallTwo)
        {
            Debug.Log("Combined");
            ballOne.gameObject.SetActive(false);
            ballTwo.gameObject.SetActive(false);
            isBallOne = false; isBallTwo = false;
            Invoke("SetPortalActive", 1f);
        }

    }

    private void SetPortalActive()
    {
        portal.gameObject.SetActive(true);
    }
}
