using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingScript : MonoBehaviour
{
    ///<summary>
    ///Jumping script to try out seperately for the main game.
    ///</summary




    #region Variables

    [Range(0, 3000)]
    [Tooltip("Meter on how strong the jump force is of the gameobject")]
    public float jumpForce,max_jump_speed;
    
    public Rigidbody rb;
    bool grounded,jumping;


    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < collision.contacts.Length; i++) {
            if (Vector3.Dot(collision.contacts[i].normal,transform.up)>0.5f) {
                grounded = true;
                jumping = false;
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.contacts.Length == 0) {
            grounded = false;
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();    
    }

    void jump() {
            jumping = true;
            rb.AddForce(transform.up * jumpForce,ForceMode.Impulse);
    }
    private void reset_jump()
    {
        if (jumping&& rb.velocity.y>0) {
            Debug.Log("Space!");
            grounded = false;
        }
    }
    void FixedUpdate()
    {
        if(Input.GetButton("Jump")&&grounded)
        {
            Debug.Log("Space");
            jump();
        }
        reset_jump();

    }

    #endregion


}
