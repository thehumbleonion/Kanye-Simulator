using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wavemanager : MonoBehaviour
{
    public GameObject detectzone;
    public GameObject[] waves;
    public int wavenum;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(checkloop());
    }

    // Update is called once per frame
    IEnumerator checkloop()
    {
        while (true)
        {
            if (detectzone == null)
            {
                if (wavenum < waves.Length)
                {
                    bool noneactive = true;
                    foreach (Transform enemy in waves[wavenum].transform)
                    {
                        if (enemy.gameObject.activeSelf)
                        {
                            noneactive = false;
                        }
                    }
                    if (noneactive)
                    {
                        try
                        {
                            waves[wavenum].SetActive(false);
                            waves[wavenum + 1].SetActive(true);
                        }
                        catch { }
                        wavenum += 1;
                    }
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
            yield return new WaitForSeconds(1);
        }
    }
}
