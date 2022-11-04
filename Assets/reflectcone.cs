using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reflectcone : MonoBehaviour
{
    public int projlayer;
    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == projlayer)
        {
            Debug.Log("jeff");
            if (other.GetComponent<Rigidbody>() != null) other.GetComponent<Rigidbody>().velocity = -other.GetComponent<Rigidbody>().velocity;
            //other.transform.LookAt(-other.transform.forward);
            
        }
    }

}
