using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearts_Flash : MonoBehaviour
{
    Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
    }

    void reduce_alpha() {
        if (img.color.a < 0) {
            CancelInvoke("reduce_alpha");
        }
        img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - 0.05f);
    }

    public void call_reduce_alpha() {
        InvokeRepeating("reduce_alpha",0,0.1f);

    }
    void increase_alpha()
    {
        if (img.color.a > 1)
        {
            CancelInvoke("increase_alpha");
        }
       
        img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + 0.05f);

    }
    public void call_increase_alpha()
    {
        Debug.Log("Increasing");
        InvokeRepeating("increase_alpha", 0, 0.1f);

    }
    // Update is called once per frame

}
