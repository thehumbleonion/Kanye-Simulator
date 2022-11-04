using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gundrop : MonoBehaviour
{
    // Start is called before the first frame update
    public int unlockint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<items>().unlockedguns[unlockint] = true;
            other.GetComponent<weaponswapper>().changegun(unlockint);
            Debug.Log("gun"+unlockint+"unlocked");
            Destroy(this.gameObject);
        }
        

    }
}
