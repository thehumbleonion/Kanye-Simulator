using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallreturn : MonoBehaviour
{
    public Transform returnpoint;
    public bool kill;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            if (kill) other.GetComponent<health>().hp = 0;
            else other.transform.position = returnpoint.transform.position;
        }
        else if (other.tag == "enemy" && kill)
        {
            try
            {
                GameObject.FindGameObjectWithTag("music").GetComponent<musicmanager>().intensity += other.GetComponent<health>().hp;
                other.GetComponent<health>().hp = 0;
            }
            catch { }
        }
    }
}
