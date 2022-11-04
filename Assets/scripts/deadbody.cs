using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadbody : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("delete", 30);
    }

    // Update is called once per frame
    void delete()
    {
        Destroy(this.gameObject);
    }
}
