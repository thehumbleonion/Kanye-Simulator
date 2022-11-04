using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class musicmanager : MonoBehaviour
{
    public float volspeed;
    public float intensity;
    public float maxintensity;
    public float lowerspeed;
    public AudioSource lowestlevel;
    public float[] levels;
    public AudioSource[] sources;
    public float[] sourcesv;
    string scene;
    public int songid;
    public AudioClip[] lowlevels;
    public AudioClip[] midlevels;
    public AudioClip[] highlevels;
    public bool transitioning;
    // Start is called before the first frame update
    // Update is called once per frame
    public int lastid;
    private void Start()
    {
        lastid = PlayerPrefs.GetInt("songid", 0);
        GameObject[] musics = GameObject.FindGameObjectsWithTag("music");
        if (musics.Length > 1)
        {
            Destroy(this.gameObject);
        }
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
    string lastscene;
    void Update()
    {
        if(lastscene != SceneManager.GetActiveScene().name)
        {
            intensity = 0;
        }
        if(PlayerPrefs.GetInt("songid", 0) != lastid)
        {
            transitioning = true;
        }
        lastid = PlayerPrefs.GetInt("songid", 0);
        if (!transitioning)
        {
            lowestlevel.volume = PlayerPrefs.GetFloat("mvolume", 1);
            int level = 0;
            intensity -= lowerspeed * Time.deltaTime;
            if (intensity < 0) intensity = 0;
            if (intensity > maxintensity) intensity = maxintensity;
            if (intensity > levels[0]) level = 1;
            if (intensity > levels[1]) level = 2;
            if (level >= 1)
            {
                sourcesv[0] += volspeed * Time.deltaTime;
                if (level >= 2)
                {
                    sourcesv[1] += volspeed * Time.deltaTime;
                }
                else
                {
                    sourcesv[1] -= volspeed * Time.deltaTime;
                }
            }
            else
            {
                sourcesv[0] -= volspeed * Time.deltaTime;
                sourcesv[1] -= volspeed * Time.deltaTime;
            }
            if (sourcesv[0] < 0) sourcesv[0] = 0;
            if (sourcesv[1] < 0) sourcesv[1] = 0;
            if (sourcesv[0] >= 1) sourcesv[0] = 1;
            if (sourcesv[1] >= 1) sourcesv[1] = 1;
            sources[0].volume = sourcesv[0] * PlayerPrefs.GetFloat("mvolume", 1);
            sources[1].volume = sourcesv[1] * PlayerPrefs.GetFloat("mvolume", 1);
        }
        else
        {
            if (lowestlevel.clip != lowlevels[lastid])
            {
                
                lowestlevel.volume -= Time.deltaTime * volspeed * 8;
                sources[0].volume -= Time.deltaTime * volspeed * 8;
                sources[1].volume -= Time.deltaTime * volspeed * 8;
                if (lowestlevel.volume + sources[0].volume + sources[1].volume < 0.01f)
                {
                    lowestlevel.clip = lowlevels[lastid];
                    sources[0].clip = midlevels[lastid];
                    sources[1].clip = highlevels[lastid];
                }
            }
            else
            {
                lowestlevel.volume += Time.deltaTime * volspeed * 8;
                if(lowestlevel.volume >= PlayerPrefs.GetFloat("mvolume", 1))
                {
                    transitioning = false;
                }
            }
            
        }
        if (!lowestlevel.isPlaying) lowestlevel.Play();
        if (!sources[0].isPlaying) sources[0].Play();
        if (!sources[1].isPlaying) sources[1].Play();
        lastscene = SceneManager.GetActiveScene().name;
    }
    IEnumerator syncloop()
    {
        while (true)
        {
            sources[0].time = lowestlevel.time;
            sources[1].time = lowestlevel.time;
            yield return new WaitForSeconds(2);
        }
    }
}
