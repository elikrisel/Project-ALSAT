using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(-100,100)]
    [SerializeField] float speed; 
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0,speed,0)*Time.deltaTime);
    }
}
