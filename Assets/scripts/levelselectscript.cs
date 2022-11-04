using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class levelselectscript : MonoBehaviour
{
    public Image[] levelbuttons;
    public Button[] actuallevlbuttons;
    public Color offcolor;
    private void Start()
    {
        string path = Application.dataPath + $"/save{PlayerPrefs.GetInt("savenum", 0)}.txt";
        items itms = GetComponent<items>();
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "maxhealth=100\nmaxstamina=100\nunlockedguns=True,False,False,False,False\nlastlevel=lv1\nlevelprogress=0");
        }
        string[] lines = File.ReadAllLines(path);
        int i = 0;
        int lvlprog = int.Parse(lines[4].Remove(0, 14));
        Debug.Log(lvlprog);
        foreach (Image s in levelbuttons)
        {
            if (lvlprog >= i)
            {
                
            }
            else
            {
                levelbuttons[i].color = offcolor;
                actuallevlbuttons[i].enabled = false;
            }
            i++;
        }
    }
    // Start is called before the first frame update
    public void Back()
    {
        SceneManager.LoadScene("main menu");
    }

    // Update is called once per frame
    public void loadlevel(string level)
    {
        string path = Application.dataPath + $"/save{PlayerPrefs.GetInt("savenum", 0)}.txt";
        Debug.Log(path);
        items itms = GetComponent<items>();
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "maxhealth=100\nmaxstamina=100\nunlockedguns=True,False,False,False,False\nlastlevel=lv1\nlevelprogress=0");
        }
        string[] lines = File.ReadAllLines(path);
        float maxhealth = int.Parse(lines[0].Remove(0, 10));
        float maxstamina = int.Parse(lines[1].Remove(0, 11));
        string[] unlockedguns = lines[2].Remove(0, 13).Split(char.Parse(","));
        int lvlprog = int.Parse(lines[4].Remove(0, 14));
        File.WriteAllText(path, $"maxhealth={maxhealth}\nmaxstamina={maxstamina}\n{lines[2]}\nlastlevel={level}\nlevelprogress={lvlprog}");
        SceneManager.LoadScene("loadscene");
    }
}
