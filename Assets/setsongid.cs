using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setsongid : MonoBehaviour
{
    public int songid;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("songid", songid);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
