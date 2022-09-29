using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Moving_Cube : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    [SerializeField] float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed * Time.deltaTime;

    }
}
