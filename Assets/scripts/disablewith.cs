using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disablewith : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject copyObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (copyObject != null) targetObject.SetActive(false);
        else targetObject.SetActive(true);
    }
}
