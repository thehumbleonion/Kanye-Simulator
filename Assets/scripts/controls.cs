using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controls : MonoBehaviour
{
    public Slider sensslide;
    private void Start()
    {
        sensslide.value = PlayerPrefs.GetFloat("sensitivity", 0.2f);
    }
    private void Update()
    {
        PlayerPrefs.SetFloat("sensitivity", sensslide.value);
    }
}
