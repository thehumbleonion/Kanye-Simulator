using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class levelupbox : MonoBehaviour
{
    public string hors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(hors == "h") GameObject.FindGameObjectWithTag("Player").GetComponent<movement>().maxhealth += 10;
            if(hors == "s") GameObject.FindGameObjectWithTag("Player").GetComponent<movement>().maxstamina += 10;
            GameObject.FindGameObjectWithTag("Player").GetComponent<movement>().Savenload(false, false, "loadscene", true, 0);
            PlayerPrefs.SetInt("levelup", 0);
        }
        
    }
}
