using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killwall : MonoBehaviour
{
    public float speed;
    public float distancespeed;
    public Transform player;
    public float x;
    public float y;
    void Update()
    {
        float distancedif = player.position.z - transform.position.z;
        if (distancedif <= 0) distancedif = 0;
        distancedif = (distancedif + 1) * distancespeed;
        transform.position += transform.forward * speed * distancedif * Time.deltaTime;
        transform.position = new Vector3(x, y, transform.position.z);
        
    }
}

