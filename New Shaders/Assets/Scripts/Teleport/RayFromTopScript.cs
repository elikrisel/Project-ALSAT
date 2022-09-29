using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RayFromTopScript : MonoBehaviour {

    #region Variables

    Transform otherPlayerTransform;
    Rigidbody otherPlayerRigidbody;
    Collider otherPlayerCollider;
    Renderer otherPlayerRenderer;

    [Tooltip("This Players' indicator"), SerializeField] Transform indicator;
    [Tooltip("This Player"), SerializeField] GameObject player;
    Grabbing setParent;
    RaycastHit hit;
    public LayerMask IgnoreMe;
    Vector3 offsetPoint;
    public GameObject particles;
    [SerializeField]float length_of_ray,vertical_movement_speed, right_stick_vertical_value;
    [SerializeField] GameObject cam,target;
    [SerializeField] Vector2 verticalRayMinMaxValue;
    [Tooltip("The max distance between players that would allow them to teleport"), SerializeField] float maxDistanceBetweenPlayers;
    
    [Tooltip("Teleport cooldown"),SerializeField] float cooldown = 1f;
    private bool isCoolingDown = false;
    #endregion

    #region Main Methods

    void Start()
    {
        right_stick_vertical_value = hinput.anyGamepad.rightStick.vertical;
        Debug.Log(transform.parent.parent.gameObject);
        GetPlayerComponents();

    }

    void FixedUpdate()
    {
        Laser();

    }

    private void Update()
    {
        if(otherPlayerTransform)
        {
            float distance = Vector3.Distance(otherPlayerTransform.position, player.transform.position);
            if (distance < maxDistanceBetweenPlayers)
            {
                TeleportInput();
            }

            Teleport();

        }
           }
    #endregion

    #region Custom Methods

    private void GetPlayerComponents()
    {
        if (player.tag == "Player")
        {
            otherPlayerCollider = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Collider>();
            otherPlayerTransform = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Transform>();
            otherPlayerRigidbody = GameObject.FindGameObjectWithTag("PlayerTwo").GetComponent<Rigidbody>();
            otherPlayerRenderer = GameObject.FindGameObjectWithTag("PlayerTwo").transform.GetChild(0).GetComponent<Renderer>();
        }
        else if (player.tag == "PlayerTwo")
        {
            otherPlayerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
            otherPlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            otherPlayerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
            otherPlayerRenderer = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<Renderer>();
        }
    }

    private void Laser()
    {
        if (player.CompareTag("Player"))
        {
            if (Mathf.Abs(hinput.gamepad[0].rightStick.vertical) > 0.3f)
            {

                right_stick_vertical_value +=  hinput.gamepad[0].rightStick.vertical* Time.deltaTime * vertical_movement_speed;
                right_stick_vertical_value = Mathf.Clamp(right_stick_vertical_value, verticalRayMinMaxValue.x, verticalRayMinMaxValue.y);
            }
        }
        if (player.CompareTag("PlayerTwo"))
        {
            if (Mathf.Abs(hinput.gamepad[1].rightStick.vertical) > 0.3f)
            {

                right_stick_vertical_value += hinput.gamepad[1].rightStick.vertical * Time.deltaTime * vertical_movement_speed;
                right_stick_vertical_value = Mathf.Clamp(right_stick_vertical_value, verticalRayMinMaxValue.x, verticalRayMinMaxValue.y);
            }
        }
              Debug.DrawRay(target.transform.position+new Vector3(0,1,0), new Vector3(cam.transform.forward.x, -cam.transform.forward.y * right_stick_vertical_value, cam.transform.forward.z) * length_of_ray, Color.red);

        if (Physics.Raycast(target.transform.position + new Vector3(0, 1, 0), new Vector3(cam.transform.forward.x, -cam.transform.forward.y * right_stick_vertical_value, cam.transform.forward.z) * length_of_ray, out hit, 20, ~IgnoreMe))
        {
            indicator.position = hit.point;
            Debug.Log(hit.collider.gameObject.name);
        }
        else
        {
            indicator.position = new Vector3(100, 10000, 0);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 10, Color.white);
        }
    }

    private void Teleport()
    {
        if (otherPlayerRigidbody.isKinematic == true && otherPlayerTransform.position != indicator.position)
        {
            
            otherPlayerTransform.SetParent(null);
            Vector3 fixPos = new Vector3(indicator.position.x, indicator.position.y + 0.15f, indicator.position.z);
            otherPlayerTransform.position = Vector3.MoveTowards(otherPlayerTransform.position, fixPos, 1 * Time.deltaTime * 100);
            if (otherPlayerTransform.position == fixPos)
            {
                otherPlayerRenderer.enabled = true;
                otherPlayerCollider.enabled = true;
                otherPlayerRigidbody.isKinematic = false;
                particles.GetComponent<ParticleSystem>().time = 0;
                particles.GetComponent<ParticleSystem>().Play();
            }
            

        }
    }

    private void TeleportInput()
    {
        if (player.tag == ("Player") && hinput.gamepad[0].Y.justPressed == true && hit.collider != null && !isCoolingDown)
        {

            StartCoroutine(TeleportCooldown());

        }

        else if (player.tag == ("PlayerTwo") && hinput.gamepad[1].Y.justPressed == true && hit.collider != null && !isCoolingDown)
        {

            StartCoroutine(TeleportCooldown());
        }
    }

    IEnumerator TeleportCooldown()
    {
        otherPlayerRigidbody.isKinematic = true;
        otherPlayerCollider.enabled = false;
        otherPlayerRenderer.enabled = false;

        offsetPoint = Vector3.Lerp(hit.point, transform.position, 0.15f);
        isCoolingDown = true;
        yield return new WaitForSeconds(cooldown);
        isCoolingDown = false;
    }
    #endregion
}
