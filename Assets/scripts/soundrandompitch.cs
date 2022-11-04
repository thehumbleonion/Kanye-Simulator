using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundrandompitch : MonoBehaviour
{
    public AudioSource audio;
    public float[] minmax;
    // Start is called before the first frame update
    void Start()
    {
        audio.volume *= PlayerPrefs.GetFloat("sfvolume", 1);
        audio.pitch = Random.Range(minmax[0], minmax[1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
