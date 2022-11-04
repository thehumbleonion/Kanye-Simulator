using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunsway : MonoBehaviour
{
    public float swayMultx = 2;
    public float swayMulty = 2;
    public float smooth = 8;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * swayMultx;
        float mouseY = Input.GetAxis("Mouse Y") * swayMulty;
        Quaternion rotationX = Quaternion.AngleAxis(-mouseX, Vector3.up);
        Quaternion rotationY = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion targetrot = rotationX * rotationY;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetrot, smooth * Time.deltaTime);
    }
}

