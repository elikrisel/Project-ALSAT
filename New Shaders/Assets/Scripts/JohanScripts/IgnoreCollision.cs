using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    [SerializeField] Transform ohterBall;
    void Start()
    {
        Physics.IgnoreCollision(transform.GetComponent<Collider>(), ohterBall.GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
