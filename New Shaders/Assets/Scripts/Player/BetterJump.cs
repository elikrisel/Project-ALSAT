using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    [Range(2,100)]
    [Tooltip("")]
    public float fallMultiplier = 2.5f;
    [Range(2,20)]
    [Tooltip("")]
    public float lowJumpMultiplier = 2f;

    public Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();    
    }
    void Update()
    {
        if(rb.velocity.y < 0)
        {
            rb.velocity = Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;


        }
       /*
        else if(rb.velocity.y > 0)
        {

            rb.velocity = Vector3.up * Physics.gravity.y * (lowJumpMultiplier + 1) * Time.deltaTime;

        }
        */


    }



}
