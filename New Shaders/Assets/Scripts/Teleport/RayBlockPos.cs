using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayBlockPos : MonoBehaviour
{
    [SerializeField]Transform player;
    [SerializeField] Transform cam;
    [SerializeField] float teleportMaxDistance = 10f;
    [SerializeField] float transitionSpeed = 2;
    float progress = 0;
    [Range(0.05f, 2f), SerializeField] float minDistanceFromPlayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(player.tag == "Player")
        {
            float stick = hinput.gamepad[0].rightStick.vertical;
            //float stick = -Input.GetAxis("CameraVertical");
            Vector3 outDir = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z) * teleportMaxDistance;
            Vector3 outPos = player.transform.position + outDir;
            outPos.y = player.transform.position.y;

            float totalSpeed = Time.deltaTime * stick * transitionSpeed;
            progress += totalSpeed;
            progress = Mathf.Clamp(progress, minDistanceFromPlayer, 1);
            transform.position = Vector3.Lerp(player.transform.position, outPos, progress);
        }
        else if(player.tag == "PlayerTwo")
        {
            float stick = hinput.gamepad[1].rightStick.vertical;
            //float stick = -Input.GetAxis("CameraVertical");
            Vector3 outDir = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z) * teleportMaxDistance;
            Vector3 outPos = player.transform.position + outDir;
            outPos.y = player.transform.position.y;

            float totalSpeed = Time.deltaTime * stick * transitionSpeed;
            progress += totalSpeed;
            progress = Mathf.Clamp(progress, minDistanceFromPlayer, 1);
            transform.position = Vector3.Lerp(player.transform.position, outPos, progress);
        }

    }
}
