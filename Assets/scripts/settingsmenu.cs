using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class settingsmenu : MonoBehaviour
{
    public GameObject canvas;
    // Start is called before the first frame update
    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("menu").Length > 1) Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
        canvas.SetActive(false);
        Time.timeScale = 1;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toggle();
        }
        
    }

    // Update is called once per frame
    public bool balls = false;
    public void toggle()
    {
        balls = !balls;
        if (balls)
        {
            canvas.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            canvas.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
