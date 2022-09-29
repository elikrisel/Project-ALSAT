using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRay : MonoBehaviour
{
    #region Variables

    [SerializeField] Transform camera;
    [SerializeField]float lerpDistance;
    [SerializeField]Transform lowRayObject;
    LayerMask ignoreMe;
    #endregion

    #region Main Methods

    void Start()
    {
        GetLayerMask();
    }


    void FixedUpdate()
    {
        Laser();
    }

    #endregion

    #region Custom Methods

    void Laser()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z), out hit, Mathf.Infinity, ~ignoreMe))
        {
            Debug.DrawRay(transform.position, new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z) * hit.distance, Color.yellow);
            Vector3 offsetPoint = Vector3.Lerp(hit.point, transform.position, lerpDistance);
            lowRayObject.position = offsetPoint/*hit.point*/;
        }
        else
        {
            Debug.DrawRay(transform.position, new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z) * 10, Color.white);

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
    private void GetLayerMask()
    {
        if (this.gameObject.tag == "Player")
        {
            ignoreMe = 1 << 8 | 1 << 10 | 1 << 12;
        }
        if (this.gameObject.tag == "PlayerTwo")
        {
            ignoreMe = 1 << 8 | 1 << 10 | 1 << 11;
        }
    }
    #endregion
}
