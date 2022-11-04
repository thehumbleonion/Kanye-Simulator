using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hood : MonoBehaviour
{
    public GameObject[] states;
    public float[] sizes;
    public int state;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        state = 0;
        if (GetComponent<health>().hp <= 200)
        {
            state = 1;
            if (GetComponent<health>().hp <= 50)
            {
                state = 2;
                if (GetComponent<health>().hp <= 0) state = 3;
            }
        }

        int i = 0;
        transform.localScale = new Vector3(1.48f, 0.1f, 0);
        foreach (GameObject s in states)
        {
            if(state == i)
            {
                transform.localScale = new Vector3(1.48f,0.1f,sizes[i]);
                s.SetActive(true);
            }
            else
            {
                s.SetActive(false);
            }
            i++;
        }
        
    }
}
