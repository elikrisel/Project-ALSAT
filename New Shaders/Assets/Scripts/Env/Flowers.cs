using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flowers : MonoBehaviour
{
    Material[] mat;
    public bool glow;
    [SerializeField] int material_index;
    [SerializeField] float rate_of_change, max_value,min_value, change_time;
    // Start is called before the first frame update
    void Start()
    {
       
        mat = GetComponent<Renderer>().materials;
        InvokeRepeating("ebb_and_flow",0,change_time);
    }

    void ebb_and_flow() {
        glow = !glow;
    }
    // Update is called once per frame
    void Update()
    {
            if (mat!=null&&glow&& mat[material_index].HasProperty("_Emission"))
            {
                mat[material_index].SetFloat("_Emission", Mathf.Lerp(mat[material_index].GetFloat("_Emission"), max_value, Time.deltaTime * rate_of_change));
            }
            else if(mat!=null && mat[material_index].HasProperty("_Emission")) { 
                mat[material_index].SetFloat("_Emission", Mathf.Lerp(mat[material_index].GetFloat("_Emission"), min_value, Time.deltaTime * rate_of_change));
            }
    }
}
