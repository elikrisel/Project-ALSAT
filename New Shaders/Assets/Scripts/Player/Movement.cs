using SharpNav.Crowds;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Base idea on smooth movement script. This is just set to 1 player for now. Can be done with 2 players.



    #region Main Components
    [SerializeField]
    private CharacterController charControl;
    Vector3 start_pos;
    Rigidbody rb;
    HealthManager health;
    GameManager gm;
   
    #endregion

    #region Variables for Movement,Jumping and Camera function
    [SerializeField]
    private float movementSpeed = 15f;
    [SerializeField]
    public float horizontal;
    [SerializeField]
    public float vertical;
    [SerializeField] float dead_zone, rotateSpeed,jump_force,max_jump_velocity,pull_force,max_character_velocity;
    Vector3 moveDirection,cam_vel;
    public bool death;
    [SerializeField] GameObject cam;
    bool jumping;
    Animator anim;
    #endregion



    #region Start method to get important components
    void Awake()
    {
        

        charControl = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        cam_vel = Vector3.zero;
        start_pos = this.transform.position;
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        health = FindObjectOfType<HealthManager>();
        gm = FindObjectOfType<GameManager>();
    }
    #endregion

    #region Collisions methods
    // Replaced this with Deathposition script. This was used for the whitebox demo.
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Lava") {
            transform.position = start_pos;
        }
        //if (collision.gameObject.CompareTag("Platform")) {
        //    transform.parent.parent = collision.gameObject.transform;
        //}
    }
    */
    private void OnCollisionStay(Collision collision)
    {
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            if (Vector3.Dot(collision.contacts[i].normal, transform.up) >0.5f)
            {
                jumping = false;
                //rb.velocity = Vector3.zero;
            }
        }
        if (collision.gameObject.CompareTag("Platform")) {
            if (check_if_stick_is_in_deadzone())
            {
                apply_platform_speeds(collision.gameObject);
            }
            else {
                add_platform_speed(collision.gameObject);
            }
        }
      
    }
    private void OnCollisionExit(Collision collision)
    {

      
        if (collision.gameObject.tag == "Platform") {
            max_character_velocity = 5;
        }
        //if (collision.gameObject.CompareTag("Platform"))
        //{
        //    transform.parent.parent = null;
        //}
    }
    #endregion

    #region Fixed Update to call functions
    void FixedUpdate() // Smooth movement and rotation
    {
        if (!death)
        {
            PlayerMovement();
            PlayerRotation();
            PlayerJump();
            clamp_to_max_speed();
        }
        else {
            Death();
        }
    }
    void Update()
    {

        bool death = false;

    }
    #endregion

    #region Custom Methods for animations and jumping and dying
    void run_and_idle_animation(float val) { 
        anim.SetFloat("IdleRun", val);
    }

    void death_anim() 
    {
        death = health;
        if(health.numberofLives <= 0)
        {
            if(health.isDead == false)
            {
                
                health.isDead = true;

            }


        }


    }
    void jumping_animation(bool val) { 
            anim.SetBool("Jump", val);
    }

    void handle_walk_and_run()
    {
        if (!jumping)
        {
            run_and_idle_animation(rb.velocity.magnitude);
        }
        else
        {
          
            run_and_idle_animation(0);
        }
    }
    void handle_jump() {
        if (jumping && !anim.GetBool("Jump"))
        {
            jumping_animation(true);
        }
        if (!jumping) { 
            jumping_animation(false);
            animation_speed(1);
        }
    }

    public void Hop() {
        Debug.Log("I'm a kangaroo!");
        anim.speed = 1.5f;
    }
    public void slow_down_jump() {
        animation_speed(1);
    }
    public void speed_up_jump()
    {
        animation_speed(7f);
    }
    void animation_speed(float speed) {
        anim.speed = speed;
    }
    public void Step() {
    }
 
    void pull_down() {
        if (jumping&&rb.velocity.y < 0) {
            apply_force(transform.up*-1,pull_force);
        }
    }
    void PlayerJump() 
    {

        if (gameObject.tag == "Player" && hinput.gamepad[0].A.pressed && !jumping) 
        {
            jumping = true;
            jump();
        }
        else if (gameObject.tag == "PlayerTwo" && hinput.gamepad[1].A.pressed && !jumping)
        {
            jumping = true;
            jump();
        }

        pull_down();
    }

    void clamp_to_max_speed() {
        if (rb.velocity.magnitude > max_character_velocity) {
            var vel = Vector3.ClampMagnitude(rb.velocity,max_character_velocity);
            rb.velocity = new Vector3(vel.x, rb.velocity.y, vel.z);
        }
    }
    public void Death()
    {
        rb.velocity = Vector3.zero;
        anim.SetBool("death", true);
        Debug.Log("I'm dead");

    }
    #endregion

    #region Custom Methods
    void move_player(Vector3 direction,float stick_pull) {
        // movement using camera direction
        Vector3 smoothMovement = movementSpeed * Time.deltaTime * direction *  Mathf.Abs(stick_pull);
        moveDirection = new Vector3(smoothMovement.x, 0f, smoothMovement.z);
        rb.velocity =  Vector3.SmoothDamp(rb.velocity,moveDirection, ref cam_vel,0.3f);
    }
    void jump() {
        apply_force(transform.up,jump_force);
    }
    void apply_force(Vector3 dir,float force) { 
        rb.AddForce(dir*force,ForceMode.Acceleration);
    }
    bool check_if_stick_is_in_deadzone() {
        if (Mathf.Abs(horizontal) < dead_zone && Mathf.Abs(vertical) < dead_zone)
        {
            return true;
        }
        return false;
    }
    void apply_platform_speeds(GameObject gb) {
     rb.velocity=gb.GetComponent<Rigidbody>().velocity;
    }
    void add_platform_speed(GameObject gb) {
        var dot = Vector3.Dot(rb.velocity.normalized, gb.GetComponent<Rigidbody>().velocity.normalized);
        Debug.Log(dot);
        if ( dot > 0.5)
        {
            max_character_velocity =10;
        }
        else { 
            max_character_velocity = 7;
        }

    }
    #endregion

    #region Player Movement and Rotation
    void PlayerMovement() // Movement
    {
        //Getting Axis for Horizontal and Vertical
        if (gameObject.tag == "Player")
        {
            horizontal = hinput.gamepad[0].leftStick.horizontal;
            vertical = hinput.gamepad[0].leftStick.vertical;
        }
        else if(gameObject.tag == "PlayerTwo")
        {
            horizontal = hinput.gamepad[1].leftStick.horizontal;
            vertical = hinput.gamepad[1].leftStick.vertical;
        }
        

        //No jumping
        Vector3 input = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z);
        Vector3 inputDirection = Camera.main.transform.TransformDirection(input);

        // smooth movement based on camera direction and how far the stick is being pulled
            var move_dir = Vector3.zero;
            move_dir += cam.transform.forward * vertical;
            move_dir += cam.transform.right * horizontal;
            move_player(move_dir, 1);
            handle_walk_and_run();
            handle_jump();


    }


    void PlayerRotation()
    {
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotateSpeed * Time.deltaTime);
        }
    }
    public void AddHealth()
    {
        health.life[health.numberofLives].gameObject.SetActive(true);
        health.numberofLives += 1;
        Debug.Log(health.numberofLives);
        // animation for gaining life in this function
    }



    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.CompareTag("Heart"))
            {
                Debug.Log("Health added");
                 health.GetComponent<HealthManager>().AddHealth();
                 Destroy(other.gameObject);
            }
    }





    #endregion




}
