using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemydoordisapear : MonoBehaviour
{
    public GameObject[] people;
    public bool canopen;
    public bool arena;
    public GameObject detectzone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (arena)
        {
            people = GameObject.FindGameObjectsWithTag("enemy");
        }
        if (detectzone == null)
        {
            if (canopen == false)
            {
                bool isnull = true;
                foreach (GameObject person in people)
                {
                    if (person != null)
                    {
                        isnull = false;
                    }
                }
                if (isnull)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
