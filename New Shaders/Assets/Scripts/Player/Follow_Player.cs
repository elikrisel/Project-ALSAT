using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Follow_Player : MonoBehaviour
{

    public GameObject target;
    [SerializeField] float follow_speed,follow_dist,follow_dist_padding,length_of_ray,vertical_movement_speed;
    [SerializeField] GameObject cam;
    // Start is called before the first frame update
    Vector3 ref_velocity = Vector3.zero;
    LayerMask layer;
    float right_stick_vertical_value;
    // Update is called once per frame
    private void Start()
    {
        layer = LayerMask.GetMask("Player");
        right_stick_vertical_value = hinput.anyGamepad.rightStick.vertical;
    }
    RaycastHit send_ray() {
        RaycastHit hit;
        Physics.Raycast(transform.position,(target.transform.position- transform.position).normalized*100, out hit, Mathf.Infinity, layer);
     
            return hit;

    }
    void FixedUpdate()
    {
        ref_velocity = target.GetComponent<Rigidbody>().velocity;
        var ray_result = send_ray();
        if (ray_result.collider&& ray_result.collider.gameObject == target&&send_ray().distance > follow_dist)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, follow_speed*Time.deltaTime);
        }
    }
}
