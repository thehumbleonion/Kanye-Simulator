using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class audio : MonoBehaviour
{
    public Slider master;
    public Slider music;
    // Start is called before the first frame update
    void Start()
    {
        music.value = PlayerPrefs.GetFloat("mvolume", 1);
        master.value = PlayerPrefs.GetFloat("sfvolume", 1);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("mvolume", music.value);
        PlayerPrefs.SetFloat("sfvolume", master.value);
    }
}
