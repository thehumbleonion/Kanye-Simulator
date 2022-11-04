using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponswapper : MonoBehaviour
{
    public Sprite[] sprites;
    public Image img;
    public int selectedgun;
    public GameObject player;
    public int lastgun = 0;
    public GameObject weaponcontroller;
    // Start is called before the first frame update
    void Start()
    {
        changegun(0);   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) changegun(lastgun);
        if (Input.GetKeyDown("1")) changegun(0);
        if (Input.GetKeyDown("2")) changegun(1);
        if (Input.GetKeyDown("3")) changegun(2);
        if (Input.GetKeyDown("4")) changegun(3);
        if (Input.GetKeyDown("5")) changegun(4);
        if (Input.GetKeyDown("6")) changegun(5);
        if (Input.GetKeyDown("7")) changegun(6);
        if (Input.GetKeyDown("8")) changegun(7);
        if (Input.GetKeyDown("9")) changegun(8);
    }

    public void changegun(int gun)
    {
        lastgun = selectedgun;
        bool change = false;
        int i = 0;
        foreach(bool unlockedgun in player.GetComponent<items>().unlockedguns)
        {
            if (i == gun && unlockedgun) change = true;i++;
        }
        if (change)
        {
            selectedgun = gun;
            i = 0;
            foreach (Transform child in weaponcontroller.transform)
            {
                if (i == selectedgun) child.gameObject.SetActive(true);
                else child.gameObject.SetActive(false);
                i++;
            }
            img.sprite = sprites[gun];
        }
    }
}
