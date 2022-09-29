using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool open;
    [SerializeField] float limit,start;
    [SerializeField] float direction;
    // Start is called before the first frame update
    private void Start()
    {
        start = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        switch (direction) {
            case 0:

        if (open && transform.position.y < limit)
        {
            transform.position += transform.right * Time.deltaTime;
        }
        else if (!open && transform.position.y > start) {
            transform.position -= transform.right * Time.deltaTime;
        }
                break;

            case 1:

                if (open && transform.position.z < limit)
                {
                    transform.position += transform.up * Time.deltaTime;
                }
                else if (!open && transform.position.z > start)
                {
                    transform.position -= transform.up * Time.deltaTime;
                }
                break;
            case 2:

                if (open && transform.position.x < limit)
                {
                    transform.position += transform.forward * Time.deltaTime;
                }
                else if (!open && transform.position.x > start)
                {
                    transform.position -= transform.forward * Time.deltaTime;
                }
                break;

        }
    }
}
