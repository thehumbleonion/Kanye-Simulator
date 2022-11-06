using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class songidtrigger : MonoBehaviour
{
    public int songid;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerPrefs.SetInt("songid", songid);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
