using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectzone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.layer == 6) Destroy(this.gameObject);
    }
}
