using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enablewith : MonoBehaviour
{
    public GameObject syncto;
    public GameObject enablesync;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enablesync.activeSelf) syncto.SetActive(true);
        else syncto.SetActive(false);
    }
}
