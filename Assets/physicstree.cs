using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class physicstree : MonoBehaviour
{
    public ConfigurableJoint joint;
    public GameObject Base;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(joint == null)
        {
            Invoke("delete", 50);
            Destroy(Base);
        }
    }
    public void Delete()
    {
        Destroy(this.gameObject);
    }
}
