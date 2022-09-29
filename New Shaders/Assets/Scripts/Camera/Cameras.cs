using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameras : MonoBehaviour
{
    // Start is called before the first frame update
    #region inspector variables

    GameObject player;
    [Header("GameObject to follow")]
    [Tooltip("the gameobject that the camera rotates with ")]
    [SerializeField] GameObject target;

    [Header("Camera speed")]
    [Tooltip("movement speed of the camera on both vertical and horizontal axis")]
    [Range(0,2)]
    [SerializeField] float camera_movement_horizontal_speed, camera_movement_vertical_speed;

    [Header("Camera movement delay")]
    [Tooltip("the delay at which the camera 'slides' into its final position")]
    [Range(-10, 10)]
    [SerializeField] float catch_up_speed, cam_follow_dist,cam_height;

    [Header("Camera vertical restrictions")]
    [Tooltip("The angles at which the camera stops moving")]
    [Range(-90, 90)]
    [SerializeField] float vert_restrictions_a,vert_restrictions_b;
    [SerializeField] float invisibility_value, invisibility_rate,visibility_rate;
    GameObject prev_inv_obj;
    public List<GameObject> objects_to_make_visible = new List<GameObject>();
    #endregion


    #region public variables
    public float stickX, stickY;
    Vector3 original_pos;
    #endregion

    #region Start
    void Start()
    {
        original_pos = transform.localPosition;
        player = target.GetComponent<Follow_Player>().target;
        visibility_rate = 1;
    }
    #endregion
    void send_ray_to_player() {
        RaycastHit hit;
        var dir = (player.transform.position - transform.position).normalized;
      
        Physics.Raycast(transform.position,dir*100,out hit);
        if (prev_inv_obj && hit.collider && prev_inv_obj != hit.collider&&!objects_to_make_visible.Contains(prev_inv_obj)&&prev_inv_obj.layer!=13)
        {
            objects_to_make_visible.Add(prev_inv_obj);
        }
        if (hit.collider&&!hit.collider.gameObject.CompareTag("Player") && !hit.collider.gameObject.CompareTag("PlayerTwo")) {
            if (hit.collider.gameObject.GetComponent<Renderer>())
            {

                var mat = hit.collider.gameObject.GetComponent<Renderer>().materials;
                prev_inv_obj = hit.collider.gameObject;
                if (mat.Length > 0)
                {
                    for (int i = 0; i < mat.Length; i++)
                    {
                        if (mat[i].HasProperty("_Invisible"))
                        {
                            mat[i].SetFloat("_Invisible", Mathf.Lerp(mat[i].GetFloat("_Invisible"), invisibility_value, Time.deltaTime * invisibility_rate));
                        }
                    }
                }
            }
        }
    }
    void make_object_visible_again() {
        for (int i = 0; i < objects_to_make_visible.Count; i++)
        {
            GameObject gb = objects_to_make_visible[i];

            if (gb.GetComponent<Renderer>())
            {
                var mat = gb.GetComponent<Renderer>().materials;
                if (mat.Length > 0)
                {
                    var count = 0;
                    for (int j = 0; j < mat.Length; j++)
                    {
                        if (mat[j].HasProperty("_Invisible"))
                        {
                            mat[j].SetFloat("_Invisible", Mathf.Lerp(mat[j].GetFloat("_Invisible"), 1, Time.deltaTime * invisibility_rate * visibility_rate));
                        }
                        if (mat[j].HasProperty("_Invisible")&&mat[j].GetFloat("_Invisible") >= 1) {
                            count += 1;
                        }
                        if (count == mat.Length) {
                            objects_to_make_visible.Remove(gb);
                        }
                    }
                }
            }
        }
    }
    #region camera_movement
    void set_rotation_values() {
        // sets the x and y rotations according to the stick movement

        if (player.gameObject.tag == "Player")
        {
                stickY += hinput.gamepad[0].rightStick.horizontal/2 * camera_movement_horizontal_speed;
                stickX += hinput.gamepad[0].rightStick.vertical/2 * camera_movement_vertical_speed;
                stickX = Mathf.Clamp(stickX, vert_restrictions_a, vert_restrictions_b);
        }
        else if(player.gameObject.tag == "PlayerTwo")
        {

                stickY += hinput.gamepad[1].rightStick.horizontal/2 * camera_movement_horizontal_speed;
                stickX += hinput.gamepad[1].rightStick.vertical/2 * camera_movement_vertical_speed;
                stickX = Mathf.Clamp(stickX, vert_restrictions_a, vert_restrictions_b);
        }
        
    }

    void move_camera() {
        // keep looking at the target
        transform.LookAt(target.transform.position);
       
        Quaternion current_rotation =target.transform.rotation;
        Quaternion target_rotation =Quaternion.Euler(stickX,-stickY,0);

        // slowly change rotation from current to updated rotation
        target.transform.rotation = Quaternion.Slerp(current_rotation,target_rotation,  Time.deltaTime*catch_up_speed);
    }
    #endregion

   
  
    #region Update
    // Update is called once per frame
    void FixedUpdate()
    {
        send_ray_to_player();
        //if the stick has moved update the target rotation
        if (Mathf.Abs(Input.GetAxis("CameraVertical")) > 0 || Mathf.Abs(Input.GetAxis("CameraHorizontal")) > 0)
        {
            set_rotation_values();
        }
        if (objects_to_make_visible.Count > 0) {
            make_object_visible_again();
        }
        // keep moving the camera even after the stick is back in the dead zone
        move_camera();
    }
    #endregion
}
