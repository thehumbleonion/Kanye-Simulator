using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilty : MonoBehaviour
{
    public GameObject prefab;
    public bool active;
    [SerializeField] private bool readytoshoot = true;
    [SerializeField] private float delay;


    void shoot()
    {
        readytoshoot = false;
        Instantiate(prefab, transform.position, transform.rotation);
        Invoke("readyshoot", delay);
    }
    void readyshoot()
    {
        readytoshoot = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse2) && readytoshoot && active)
        {
            shoot();
        }
    }
}
