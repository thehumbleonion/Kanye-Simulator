using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    public LineRenderer balls;
    public float extra;
    public GameObject laserorigin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        balls.SetPosition(0, transform.position);
        if(Physics.Raycast(laserorigin.transform.position, laserorigin.transform.forward,out hit))
        {
            balls.SetPosition(1, hit.point + (transform.forward * extra));
        }
        else
        {
            balls.SetPosition(1, transform.forward*100);
            //Debug.Log("gayballs");
        }
    }
}
