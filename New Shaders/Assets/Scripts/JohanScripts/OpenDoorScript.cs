using DungeonArchitect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorScript : MonoBehaviour
{
    [Tooltip("'left', 'right', 'up', 'down', 'forward' or 'backward'"),SerializeField] string openDoorDirection;
    [SerializeField] float limit;
    [SerializeField] bool open;
    [SerializeField] float openSpeed;
    // Start is called before the first frame update
    void Start()
    {
        open = false;
        openDoorDirection.ToLower();
        getDoorDirection();
    }
    #region getDoorDirection
    private void getDoorDirection()
    {
        switch (openDoorDirection)
        {
            case "left":
                limit = transform.position.x - limit;
                break;
            case "right":
                limit += transform.position.x;
                break;
            case "up":
                limit += transform.position.y;
                break;
            case "down":
                limit = transform.position.y - limit;
                break;
            case "forward":
                limit += transform.position.z;
                break;
            case "backward":
                limit = transform.position.z - limit;
                break;
            default:
                break;
        }
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        SlideOpenDoor();
    }

    private void SlideOpenDoor()
    {
        if (!open && openDoorDirection == "left" && transform.position.x > limit)
        {
            transform.position += -transform.right * Time.deltaTime * openSpeed;
        }
        else if (!open && openDoorDirection == "right" && transform.position.x < limit)
        {
            transform.position += transform.right * Time.deltaTime * openSpeed;
        }
        else if (!open && openDoorDirection == "up" && transform.position.y < limit)
        {
            transform.position += transform.up * Time.deltaTime * openSpeed;
        }
        else if (!open && openDoorDirection == "down" && transform.position.y > limit)
        {
            transform.position += -transform.right * Time.deltaTime * openSpeed;
        }
        else if (!open && openDoorDirection == "forward" && transform.position.z < limit)
        {
            transform.position += transform.forward * Time.deltaTime * openSpeed;
        }
        else if (!open && openDoorDirection == "backward" && transform.position.z > limit)
        {
            transform.position += -transform.forward * Time.deltaTime * openSpeed;
        }
        else
        {
            open = true;
        }
    }
}
