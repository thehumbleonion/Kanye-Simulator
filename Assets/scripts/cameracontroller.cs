using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameracontroller : MonoBehaviour
{
    public float sensitivity;
    public float scopesensitivitymult;
    public float scopedfov;
    public bool scoped;
    public GameObject player;
    float xRotation = 0f;
    float yRotation = 0f;
    float startfov;
    public bool returnto0;
    Camera cam;
    settingsmenu menu;
    float timestill;
    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.FindGameObjectWithTag("menu").GetComponent<settingsmenu>();
        cam = GetComponent<Camera>();
        startfov = cam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (!menu.balls)
        {
            float sens = PlayerPrefs.GetFloat("sensitivity", 0.2f);
            cam.fieldOfView = startfov;
            if (scoped)
            {
                sens = sensitivity * scopesensitivitymult;
                cam.fieldOfView = scopedfov;
            }
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            float x = Input.GetAxis("Mouse X") * sens * Time.deltaTime * 1000;
            float y = Input.GetAxis("Mouse Y") * sens * Time.deltaTime * 1000;
            
            if (returnto0)
            {
                xRotation -= y;
                yRotation += x;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);
                if (Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0)
                {
                    timestill += Time.deltaTime;
                }
                else
                {
                    timestill = 0;
                }
                if (timestill > 1.5)
                {
                    transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 0), 3*Time.deltaTime);
                    xRotation = 0;
                    yRotation = 0;
                }
                else
                {
                    player.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
                }

            }
            else
            {
                xRotation -= y;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);
                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                player.transform.Rotate(player.transform.up * x);
            }
        }
        
    }
}
