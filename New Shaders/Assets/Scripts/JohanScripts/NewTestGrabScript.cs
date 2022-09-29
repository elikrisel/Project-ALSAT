using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class NewTestGrabScript : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField]GameObject pickedObject;
    bool grabbed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 outDir = new Vector3(transform.forward.x, 0, transform.forward.z);
        Vector3 outPos = transform.position + outDir * 1.5f;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1.5f) && !grabbed && hinput.gamepad[0].leftTrigger.pressed)
        {
            if (hit.transform.gameObject.CompareTag("Grab") && pickedObject == null)
            {
                pickedObject = hit.collider.gameObject;
                pickedObject.transform.position = outPos;
                grabbed = true;

            }
        }
        if (this.transform.CompareTag("Player"))
        {
            if (grabbed && hinput.gamepad[0].leftTrigger.pressed)
            {
                pickedObject.transform.position = outPos;
                Destroy(pickedObject.GetComponent<Rigidbody>());
                pickedObject.GetComponent<Collider>().isTrigger = true;
            }
            if (grabbed && hinput.gamepad[0].leftTrigger.released)
            {
                grabbed = false;
                pickedObject.AddComponent<Rigidbody>();
                pickedObject.GetComponent<Collider>().isTrigger = false;
                pickedObject = null;
            }
        }
        if (this.transform.CompareTag("PlayerTwo"))
        {
            if (grabbed && hinput.gamepad[1].leftTrigger.pressed)
            {
                pickedObject.transform.position = outPos;
                Destroy(pickedObject.GetComponent<Rigidbody>());
                pickedObject.GetComponent<Collider>().enabled = false;
            }
            if (grabbed && hinput.gamepad[1].leftTrigger.released)
            {
                grabbed = false;
                pickedObject.AddComponent<Rigidbody>();
                pickedObject.GetComponent<Collider>().enabled = true;
                pickedObject = null;
            }
        }


    }
}
