using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectremovedanim : MonoBehaviour
{
    [SerializeField] private GameObject Object;
    [SerializeField] private Animator anim;
    private void Update()
    {
        if(Object == null)
        {
            anim.SetBool("playanim", true);
        }
    }
}
