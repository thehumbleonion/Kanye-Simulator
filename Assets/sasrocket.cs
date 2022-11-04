using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sasrocket : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody jet;
    public float rotationspeed = 10;
    public float speedadvantage;
    public Rigidbody rb;
    void Start()
    {
        jet = GameObject.FindGameObjectWithTag("jet").GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    float jetspeed;
    void Update()
    {
        try { jetspeed = jet.velocity.magnitude; } catch { }
        rb.velocity = transform.forward * (speedadvantage+jetspeed);
        Vector3 lookPos = jet.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationspeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<health>().hp = 0;
    }
}
