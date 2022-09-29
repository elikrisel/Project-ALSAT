using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fish : MonoBehaviour
{
    [SerializeField] float range_for_point_selection;
    [SerializeField] float probability_of_chasing_player;
    [SerializeField] float jump_distance;
    NavMeshAgent nav;
    Transform[] players;
    Transform lucky_player;
    bool attacking, jumping, swimming, ground_touched, surfacing;
    Rigidbody rb;
    Vector3 start_pos, surface_point;
    Animator anim;
    bool rampage;
    GameObject healthbar;
    [SerializeField] float surface_speed;
    [SerializeField] GameObject particle_effect;
    float return_height;
    [SerializeField] float clamp_velocity;
    // Start is called before the first frame update
    void Start()
    {
        start_pos = select_patrol_point();
        rampage = false;
        nav = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        select_patrol_point();
        players = new Transform[] { GameObject.FindGameObjectWithTag("Player").transform, GameObject.FindGameObjectWithTag("PlayerTwo").transform };
        attacking = false;
        jumping = false;
        swimming = true;
        ground_touched = false;
        surfacing = false;
        surface_point = Vector3.zero;
        set_point_selection_range(10);
        healthbar = GameObject.Find("HealthBar");
        InvokeRepeating("select_player_to_chase", 0, 5);
        return_height = transform.position.y;
    }
    void set_point_selection_range(int range) { 
        range_for_point_selection = range;
    }
    void select_player_to_chase()
    {
        var random = Random.Range(0, 2);
        if (random == 0)
        {
            lucky_player = players[0];
        }
        else
        {
            lucky_player = players[1];
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerTwo"))
        {
            healthbar.GetComponent<HealthManager>().TakeDamage(1);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && check_if_attack_over() && !IsInvoking("set_swimming_true")) {
            attacking = false;
            anim.SetTrigger("Panic");
            Invoke("set_grounded_true", 2);
            Invoke("set_swimming_true", 2);
        }
    }

    void clamp_speed() {
        if (!attacking) {
            Vector3.ClampMagnitude(rb.velocity, clamp_velocity);
        }
    }
    void set_grounded_true() {
        ground_touched = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            set_point_selection_range(50);
            start_pos = select_patrol_point();

            particle_effect.SetActive(true);
            particle_effect.GetComponent<ParticleSystem>().time = 0;
            particle_effect.GetComponent<ParticleSystem>().Play();
        }
    }
  
    void set_all_anim_false() {
        anim.SetBool("Idle", false);
        anim.SetBool("Takeoff", false);
        anim.SetBool("Fall", false);
    }
    void set_swimming_true() {
        swimming = true;
    }
    bool check_if_attack_over()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.3f) {
            return true;
        }
        return false;
    }
    void reset_navmesh() {
        surfacing = false;
        rb.velocity = Vector3.zero;
        nav.enabled = true;
        ground_touched = false;
        attacking = false;
    }
    void unset_navmesh()
    {
        nav.enabled = false;
    }
    void set_jumping() {
        jumping = true;
    }
    void unset_jumping() {
        jumping = false;
    }
    bool check_if_surfaced() {
        if (transform.position.y < return_height+0.5f) {
            return false;
        }
        reset_navmesh();
        unset_jumping();
        return true;
    }
    void check_if_surfaced_and_still_flapping() {
        if (nav.enabled && surfacing) {
            surfacing = false;
        }
    }
    public void start_surfacing() {
        transform.position = Vector3.MoveTowards(transform.position, start_pos + new Vector3(0, 1, 0), Time.deltaTime * surface_speed);
    }
    Vector3 select_patrol_point() {
        Vector3 random_dir = Random.insideUnitSphere * range_for_point_selection;
        random_dir += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(random_dir, out hit, range_for_point_selection, 1);
        return hit.position;
    }

    void move_over_to_player(Vector3 target_pos) {
        nav.SetDestination(new Vector3(target_pos.x, transform.position.y, target_pos.z));
    }
    void attack() {
        if (distance_to_jump_at() && !jumping)
        {
            Debug.Log(transform.position);
            jump();
            attack_anim();
        }
        else {
            move_over_to_player(lucky_player.position);
        }
    }
    void look_at_pos(Vector3 pos,float speed) {
        var targetRotation = Quaternion.LookRotation(pos- transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime*speed);

    }
    void attack_anim() {
        set_all_anim_false();
        anim.SetTrigger("Prepare");
    }
    void idle_anim() {
        set_all_anim_false();
        anim.SetBool("Idle", true);
    }
    Vector3 CalculateTrajectoryVelocity(Vector3 origin, Vector3 target, float t)
    {
        float vx = (target.x - origin.x) / t;
        float vz = (target.z - origin.z) / t;
        float vy = ((target.y - origin.y) - 0.5f * Physics.gravity.y * t * t) / t;
        return new Vector3(vx, vy, vz);
    }
    void jump() {
        set_jumping();
        unset_navmesh();
        Vector3 vector = CalculateTrajectoryVelocity(transform.position,lucky_player.position, 3);
        rb.velocity = vector;
    }
   
    bool distance_to_jump_at() {
        if (Vector2.Distance(new Vector2(transform.position.x,transform.position.z),new Vector2(lucky_player.position.x,lucky_player.position.z))<jump_distance){
            return true;
        }
        return false;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        clamp_speed();
        if (ground_touched && !check_if_surfaced())
        {
            surfacing = true;
            look_at_pos(start_pos,200);
            idle_anim();
            start_surfacing();
        }
        check_if_surfaced_and_still_flapping();
        if (attacking) { 
                look_at_pos(lucky_player.transform.position,50);
        }
        if (Mathf.Abs(transform.position.magnitude - nav.destination.magnitude)<1f) {
            var chance = Random.Range(0, 10);
            if (chance < probability_of_chasing_player||(attacking))
            {
                attacking = true;
                swimming = false;
                attack();
            }
            else if(!attacking)
            {
                var move_pos = select_patrol_point();
                move_over_to_player(move_pos);
            }
        }
    }
}
