using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthpickup : MonoBehaviour
{
    public float hp;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<health>().hp += hp;
            Destroy(this.gameObject);
        }
    }
}
