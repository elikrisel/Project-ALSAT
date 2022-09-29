using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    [SerializeField] GameObject cam;
    // Start is called before the first frame update
  
    void decide_distance() { 
         transform.position+=new Vector3(0,0, cam.GetComponent<Cameras>().stickY/100) *Time.deltaTime;
    }
    // Update is called once per frame
    void Update()
    {
        decide_distance();
    }
}
