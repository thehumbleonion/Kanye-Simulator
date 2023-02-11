using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class weapon : MonoBehaviour
{
    public bool ricoche;
    public GameObject projectile;
    public Transform tip;
    public LayerMask layermask;
    public string ammotype;
    public float speedmult;
    public float timebeforeshot;
    public float jumpmult;
    public bool buttonhold;
    public float firerate;
    public float spread;
    public int shotsper = 1;
    public float damage;
    public float speed;
    public bool canshoot = true;
    public float rotatedis;
    Quaternion tipstartrotation;
    public bool tiprotate;
    public Animator anim;
    public bool scopable;
    public GameObject scopesprite;
    public GameObject ui;
    public float camforwardoffset = 0.6f;
    public AudioSource audio;
    public bool spaceshoot;
    settingsmenu menu;
    float startvol;
    public bool jet;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("menu") == null)
        {
            SceneManager.LoadScene("main menu");
        }
        menu = GameObject.FindGameObjectWithTag("menu").GetComponent<settingsmenu>();
        tipstartrotation = tip.localRotation;
        startvol = audio.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!menu.balls)
        {
            audio.volume = startvol* PlayerPrefs.GetFloat("sfvolume", 1);
            else
            {
                try { cam.GetComponent<cameracontroller>().scoped = false; } catch { }
                guncam.SetActive(true);
            }
            RaycastHit hit;

            if (Physics.Raycast(cam.position, cam.forward, out hit, Mathf.Infinity, layermask))
            {
                if (Vector3.Distance(cam.position, hit.point) > rotatedis)
                {
                    tip.LookAt(hit.point);
                }
                else tip.localRotation = tipstartrotation;

            }
            else tip.localRotation = tipstartrotation;
            bool press = false ;
            bool pressed = false ;
            if (spaceshoot & Input.GetKeyDown(KeyCode.Space)) press = true;
            if (Input.GetKeyDown(KeyCode.Mouse0)) press = true;
            if (spaceshoot & Input.GetKey(KeyCode.Space)) pressed = true;
            if (Input.GetKey(KeyCode.Mouse0)) pressed = true;
            if ((pressed && buttonhold) || (press && !buttonhold)) //PlayerPrefs.GetInt(ammotype, 0) > 0
            {
                if (canshoot)
                {
                    Invoke("Shoot", timebeforeshot);
                }

            }
        }
        
    }
    void Shoot()
    {

        anim.Play("shoot");
        canshoot = false;
        audio.Play();
        for(int i = 0; i < shotsper; i++)
        {
            Transform spawnpoint = cam;
            if (tiprotate)
            {
                spawnpoint = tip;
            }
            Quaternion rot = Quaternion.Euler(spawnpoint.rotation.eulerAngles + new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), 0f));
            GameObject proj = Instantiate(projectile, spawnpoint.position + (spawnpoint.transform.forward*camforwardoffset), rot);
            proj.GetComponent<Rigidbody>().velocity = proj.transform.forward * speed;
            proj.GetComponent<projectile>().damage = damage;
            proj.GetComponent<projectile>().playerprojectile = true;
            proj.layer = 11;
            proj.GetComponent<projectile>().weapon = this.gameObject;
            if (!tiprotate)
            {
                GameObject fproj = Instantiate(projectile, tip.position, rot);
                fproj.GetComponent<projectile>().speed = speed * 0.8f;
                fproj.GetComponent<projectile>().follower = true;
                fproj.GetComponent<projectile>().followed = proj;
                if(jet) fproj.layer = 3;
                fproj.layer = 11;
            }
        }
        Invoke("canshoottrue", firerate);
    }
    void canshoottrue()
    {
        canshoot = true;
    }
}
