using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagebox : MonoBehaviour
{
    public float damage;
    public List<Collider> damaged;
    public bool dodamage;
    // Start is called before the first frame update
    void Update()
    {
        if(!dodamage) damaged = new List<Collider>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (dodamage)
        {
            bool dontdamage = false;
            foreach (Collider col in damaged)
            {
                if (col == other)
                {
                   if(col.gameObject.tag != "nomelee") dontdamage = true;
                }
            }
            if (other.GetComponent<health>() != null && other.GetComponent<damagebox>() == null && !dontdamage)
            {
                other.GetComponent<health>().hp -= damage;
                damaged.Add(other);
            }
        }
        
    }
}
