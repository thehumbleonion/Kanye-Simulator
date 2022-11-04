using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class loadscene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string path = Application.dataPath + $"/save{PlayerPrefs.GetInt("savenum", 0)}.txt";
        Debug.Log(path);
        items itms = GetComponent<items>();
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "maxhealth=100\nmaxstamina=100\nunlockedguns=True,False,False,False,False\nlastlevel=lvl1\nlevelprogress=0");
        }
        string[] lines = File.ReadAllLines(path);
        if (PlayerPrefs.GetInt("levelup", 0) == 1)
        {
            SceneManager.LoadScene("levelup");
        }
        else
        {
            SceneManager.LoadScene(lines[3].Remove(0, 10));
        }
    }

    // Update is called once per frame
}
