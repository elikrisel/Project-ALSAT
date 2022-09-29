using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayFromPlayer : MonoBehaviour {
    Transform target;
    public LayerMask IgnorePlayerLayer;
    Transform lowRayObject;
    Vector3 rayOrigin;
    float lerpDistance;
    void Start()
    {
        
        //lowRayObject = GameObject.FindGameObjectWithTag("LowRayObject").GetComponent<Transform>();
        lowRayObject = transform.Find("LowRayPoint");
        FindPlayer();

    }

    void Update()
    {
        rayOrigin = transform.position;

        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 20, ~IgnorePlayerLayer))
        {
            Vector3 offsetPoint = Vector3.Lerp(hit.point, rayOrigin, lerpDistance);
            lowRayObject.position = offsetPoint/*hit.point*/;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        }
        else
        {

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.white);
            Debug.Log("Did not Hit");
        }
        CheckDistance(hit);

    }

    private void CheckDistance(RaycastHit hit)
    {
        if (hit.distance < 4 && hit.distance > 3)
        {
            lerpDistance = 0.2f;
        }
        else if (hit.distance < 3)
        {
            lerpDistance = 0.3f;
        }
        else
        {
            lerpDistance = 0.15f;
        }
    }
    private void FindPlayer()
    {
        if (this.gameObject.tag == "Player")
        {
            target = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Transform>();
        }
        else if (this.gameObject.tag == "PlayerTwo")
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

    }
}

