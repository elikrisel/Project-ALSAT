using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour
{
    [Header("Range of grab")]
    [Range(0,2)]
    [SerializeField] float limit;
    [Header("Force to apply when throwing")]
    [SerializeField] float force_x, force_y;
    public GameObject grabbed_obj;
    [SerializeField] Transform target_pos;
    bool grab;
    public bool on_platform_grabbed;
    // Start is called before the first frame update
    void Start()
    {
        grab = false;
    }


    RaycastHit ray_cast() {
        RaycastHit hit;
        Physics.Raycast(transform.position,transform.forward, out hit,limit);
        Debug.DrawRay(transform.position, transform.forward, Color.white);
        return hit;
    }

    bool check_if_grabbable() {
        var hit = ray_cast();
        if (hit.collider)
        {
            if (grabbed_obj == hit.collider.gameObject) {
                return true;
            }
            grabbed_obj = hit.collider.gameObject;
            if (grabbed_obj.CompareTag("Grab") &&( Vector3.Dot(this.transform.forward, grabbed_obj.transform.position - this.transform.position)) > 0.5f && grabbed_obj.CompareTag("Grab"))
            {
                return true;
            }
        }
        grabbed_obj = null;
        return false;
    }

    void null_parent() {
        if (grabbed_obj&& grabbed_obj.transform.parent != this.transform) {
            grabbed_obj.transform.parent = null;
        }
    }
    void let_go_of_the_other_objects() {
        Debug.Log(transform.childCount);

        if (transform.childCount > 3) {
            for (int i = 2; i < transform.childCount; i++) {
                transform.GetChild(i).transform.parent = null;
            }
        }
    }
    void grab_object() {
        null_parent();
        if (check_if_grabbable()) {
            grab = true;
            grabbed_obj.transform.parent = this.transform;
            grabbed_obj.GetComponent<Grab_Status>().grabbed = true;
            //grabbed_obj.GetComponent<Rigidbody>().isKinematic = true;
            Destroy(grabbed_obj.GetComponent<Rigidbody>());
            let_go_of_the_other_objects();

        }
    }
    void drop_obj() {
        if (grabbed_obj) {
            grabbed_obj.transform.parent = null;
            grabbed_obj.GetComponent<Grab_Status>().grabbed = false;
        }
    }
    public void set_parent_to_this() {
        if (grabbed_obj) {
            grabbed_obj.transform.parent = this.transform;
        }
    }
    void throw_obj() {
        if (grabbed_obj) {
            grabbed_obj.GetComponent<Grab_Status>().grabbed = false;
            grabbed_obj.transform.parent = null;
            Vector3 vector = CalculateTrajectoryVelocity(transform.position, target_pos.position, 2);
            grabbed_obj.AddComponent<Rigidbody>();
            grabbed_obj.GetComponent<Rigidbody>().velocity = vector;
        }
    }

    Vector3 CalculateTrajectoryVelocity(Vector3 origin, Vector3 target, float t)
    {
        float vx = (target.x - origin.x) / t;
        float vz = (target.z - origin.z) / t;
        float vy = ((target.y - origin.y) - 0.5f * Physics.gravity.y * t * t) / t;
        return new Vector3(vx, vy, vz);
    }
    void reset_grab() {
        grab = false;
        //Debug.Log("no grab");
        grabbed_obj.AddComponent<Rigidbody>();
        //grabbed_obj.GetComponent<Rigidbody>().isKinematic = false;
        grabbed_obj = null;
    }
    void player_control_for_grabbing(int index) {
        if (hinput.gamepad[index].rightTrigger.pressed && !hinput.gamepad[index].rightBumper.pressed )
        {
            grab_object();
           // Debug.Log("grab");
        }
        else if (grab && !hinput.gamepad[index].rightTrigger.pressed)
        {
            drop_obj();
            reset_grab();
        }
        else if (grab && hinput.gamepad[index].rightBumper.pressed)
        {
            throw_obj();
            reset_grab();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(this.gameObject.tag == "Player")
        {
            player_control_for_grabbing(0);
        }
        if (this.gameObject.tag == "PlayerTwo")
        {
            player_control_for_grabbing(1);
        }
    }
}
