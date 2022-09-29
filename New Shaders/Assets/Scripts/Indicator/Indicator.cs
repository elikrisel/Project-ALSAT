using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    [SerializeField] Renderer[] mt;
    [SerializeField] string center_name;
    private MaterialPropertyBlock _propBlock;
    // Update is called once per frame

    private void Start()
    {

        _propBlock = new MaterialPropertyBlock();
        mt = FindObjectsOfType<Renderer>();

    }
    void FixedUpdate()
    {
        for (int i = 0; i < mt.Length; i++)
        {
        
            {
                mt[i].GetPropertyBlock(_propBlock);
                _propBlock.SetVector(center_name, transform.position);
                mt[i].SetPropertyBlock(_propBlock);
            }
        }
    }
}
