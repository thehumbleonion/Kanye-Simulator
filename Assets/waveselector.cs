using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveselector : MonoBehaviour
{
    public GameObject visable;
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("wave").Length > 0)
        {
            visable.SetActive(false);
            PlayerPrefs.SetInt("songid", 8);
        }
        else
        {
            visable.SetActive(true);
            PlayerPrefs.SetInt("songid", 9);
        }
    }
}
