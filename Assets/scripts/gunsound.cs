using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunsound : MonoBehaviour
{
    public bool shoot;
    public AudioSource audio;
    float startvol;
    private void Start()
    {
        startvol = audio.volume;
    }
    // Start is called before the first frame update
    bool off;
    void Update()
    {
        audio.volume = startvol * PlayerPrefs.GetFloat("sfvolume", 1);
        if (!shoot)
        {
            off = true;
        }
        if (off && shoot)
        {
            off = false;
            audio.Play();
        }
    }

    // Update is called once per frame
    void Shoot()
    {
        
    }
}
