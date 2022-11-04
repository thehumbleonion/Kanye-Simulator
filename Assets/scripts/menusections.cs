using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menusections : MonoBehaviour
{
    public int section;
    public GameObject[] sections;
    public GameObject settings;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void changescene(string scene)
    {
        SceneManager.LoadScene(scene);
        settings.GetComponent<settingsmenu>().toggle();
        Destroy(settings);
    }
    void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        int i = 0;
        foreach(GameObject g in sections)
        {
            if (i == section)
            {
                g.SetActive(true);
            }
            else
            {
                g.SetActive(false);
            }
            i++;
        }
    }
    public void setsection(int secs)
    {
        section = secs;
    }
}
