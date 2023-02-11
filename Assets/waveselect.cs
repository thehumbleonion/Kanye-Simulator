using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveselect : MonoBehaviour
{

    float prevhealth;
    [SerializeField] private GameObject prefab;
    void Update()
    {
        if(prevhealth > GetComponent<health>().hp)
        {
            Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
            GetComponent<health>().hp = 9999;
        }
        prevhealth = GetComponent<health>().hp;
    }
}
