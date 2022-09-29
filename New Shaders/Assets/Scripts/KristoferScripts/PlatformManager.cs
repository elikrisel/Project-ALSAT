using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    GameObject platform;



    #region Respawn and Despawn Platform
    void Respawn()
    {
        platform.SetActive(true);
        
    }

    void Despawn() 
    {
        platform.SetActive(false);
        
    }
    #endregion

    public void cancel_functions() {
        CancelInvoke("Despawn");
        CancelInvoke("Respawn");
    }
    #region Call Respawn function
    public void CallRespawn(GameObject platforms,float fallTimer,float respawnTimer)
    {
        platform=platforms;
        Invoke("Despawn", fallTimer);
        Invoke("Respawn", respawnTimer);
       
    }
    #endregion


}
