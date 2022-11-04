using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainmenuscript : MonoBehaviour
{
    // Start is called before the first frame update
    public int lint = 0;
    public Text inttext;
    private void Start()
    {
        PlayerPrefs.SetInt("levelup", 0);
    }
    public void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void up()
    {
        lint += 1;
        inttext.text = $"{lint}";
    }
    public void down()
    {
        if (lint - 1 >= 0)
        {
            lint -= 1;
        }
        inttext.text = $"{lint}";
    }
    public void load()
    {
        PlayerPrefs.SetInt("savenum", lint);
        SceneManager.LoadScene("levelselect");
    }
}
