using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Cube_Generator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed;
    Rigidbody rb;
    [SerializeField] GameObject cube;
    [SerializeField] float start_time, repeat_rate;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("Spawn_Cube", start_time, repeat_rate);
    }

    void Spawn_Cube() {
        Instantiate(cube, transform.position, transform.rotation);
    }
    // Update is called once per frame
 
}
