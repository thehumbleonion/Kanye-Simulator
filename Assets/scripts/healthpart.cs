using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthpart : MonoBehaviour
{
    // Start is called before the first frame update
    float starthealth;
    public float healthminus;
    public float damagemult;
    void Start()
    {
        starthealth = GetComponent<health>().hp;
    }

    // Update is called once per frame
    void Update()
    {
        healthminus = (starthealth - GetComponent<health>().hp) * damagemult; 
    }
}
