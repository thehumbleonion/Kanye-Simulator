using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosive : MonoBehaviour
{
    public float explosionradius;
    public float explosiondamage;
    public float explosionforce;
    public GameObject fire;
    public bool explodeonimpact;
    public float explosionwaittime;
    public bool policecar;
    public GameObject policecartarget;
    public GameObject car;
    public bool onlyplayer;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (explodeonimpact)
        {
            fire.SetActive(true);
            fire.transform.parent = null;
            explode();
        }
    }

    // Update is called once per frame
    bool exploded = false;
    void Update()
    {
        if (!explodeonimpact)
        {
            if (GetComponent<health>().hp <= 0 && !exploded)
            {
                fire.SetActive(true);
                if (policecar)
                {
                    car.GetComponent<policecarai>().player = policecartarget;
                }
                exploded = true;
                Invoke("explode", explosionwaittime);
            }
        }
    }
    void explode()
    {
        exploded = true;
        Collider[] affectedObs = Physics.OverlapSphere(transform.position, explosionradius);
        foreach (Collider ob in affectedObs)
        {
            if (ob.GetComponent<health>() != null)
            {
                if(!onlyplayer) ob.GetComponent<health>().hp -= explosiondamage;
                if (onlyplayer && ob.gameObject.tag != "enemy") ob.GetComponent<health>().hp -= explosiondamage;
            }
            Rigidbody obRb = ob.GetComponent<Rigidbody>();

            if (obRb != null)
            {
                obRb.AddExplosionForce(explosionforce, transform.position, explosionradius);
            }
        }
        Destroy(this.gameObject);
        if (policecar)
        {
            car.GetComponent<health>().hp = 0;
        }
    }
}
