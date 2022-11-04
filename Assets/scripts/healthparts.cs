using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthparts : MonoBehaviour
{
    public float hp;
    public healthpart[] parts;
    float starthp;
    // Start is called before the first frame update
    void Start()
    {
        starthp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        float minushps = 0;
        foreach (healthpart part in parts)
        {
            minushps += part.healthminus;
        }
        hp = starthp - minushps;
    }
}
