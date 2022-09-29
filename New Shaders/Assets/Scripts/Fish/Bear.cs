using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Bear : MonoBehaviour
{
    public bool breaking_out;
    [SerializeField] float breakout_force;
    NavMeshAgent nav;
    Transform[] players;
    Transform lucky_player;
    bool attack,wait_for_barrier;
    Animator anim;
    GameObject healthbar;
   [SerializeField] List<GameObject> fences = new List<GameObject>();
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    public bool rampage;
    [SerializeField] GameObject particle_effect;
    // Start is called before the first frame update

    void Start()
    {
        rampage = false;
        nav = GetComponent<NavMeshAgent>();
        players=new Transform[] { GameObject.FindGameObjectWithTag("Player").transform,GameObject.FindGameObjectWithTag("PlayerTwo").transform};
        InvokeRepeating("select_player_to_chase", 0, 10);
        anim = GetComponent<Animator>();
        healthbar = GameObject.Find("HealthBar");
        Debug.Log(healthbar);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fence")) {
            if (!fences.Contains(collision.gameObject)) {
                collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                fences.Add(collision.gameObject);
            }
            attack_anim();
            wait_for_barrier = true;
            chase_players(this.transform.position);
            Invoke("reset_wait_for_barrier",1.5f);
        }
     
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerTwo") && !enemies.Contains(collision.gameObject))
        {
            enemies.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerTwo")) {
            enemies.Remove(collision.gameObject);
        }
    }
    void reset_wait_for_barrier() {
        wait_for_barrier = false;
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
        if (rampage)
        {
            chase_players(lucky_player.transform.position);
        }
    }
    void break_out(GameObject gb)
    {
        if (wait_for_barrier)
        {
            var dir = (gb.transform.position - transform.position).normalized;
            gb.GetComponent<Rigidbody>().AddForce(dir * breakout_force, ForceMode.Impulse);
        }
    }

    RaycastHit raycast_to_obj(GameObject start, GameObject target)
    {
        RaycastHit hit;
        var dir = (target.transform.position - start.transform.position).normalized;
        Physics.Raycast(transform.position, dir * 5, out hit);
        Debug.DrawRay(transform.position, dir * 5);
        return hit;
    }
    bool check_distance()
    {
        RaycastHit hit= raycast_to_obj(this.gameObject, lucky_player.gameObject);
        if (hit.collider&&(hit.collider.gameObject.CompareTag("Player")|| hit.collider.gameObject.CompareTag("PlayerTwo")) && hit.distance < 2)
        {
            return true;
        }
        return false;


    }
    void chase_players(Vector3 pos)
    {
        nav.SetDestination(new Vector3(pos.x, transform.position.y, pos.z));
    }
    void attack_player() {
        Debug.Log("Attack");
        chase_players(transform.position);
        attack_anim();
    }
    public void set_attack_to_false() {
        anim.SetBool("attack", false);
    }
   
    public void throw_fence()
    {
        if (fences.Count != 0)
        {


            for (int i = 0; i < fences.Count; i++)
            {
                break_out(fences[i]);
            }

            fences.Clear();
        }
    }

    public void damage_players() {
        for (int i = 0; i < enemies.Count; i++) {
            healthbar.GetComponent<HealthManager>().TakeDamage(1);
        }
    }
    public void ground_particles() {
        particle_effect.GetComponent<ParticleSystem>().time = 0;
        particle_effect.GetComponent<ParticleSystem>().Play();
    }
    
    void chase_anim() {
        anim.SetBool("walk", true);
    }
    void attack_anim() {
        anim.SetBool("attack", true);
    }
    // Update is called once per frame
    void Update()
    {
        if (rampage)
        {
            if (check_distance())
            {
                attack_player();
            }
            else if (nav.destination != lucky_player.transform.position)
            {
                chase_players(lucky_player.transform.position);
                chase_anim();
            }
        }
    }

}
