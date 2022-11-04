using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class enemyweapon : MonoBehaviour
{
    public bool rocketlauncherdisapear;
    public Transform rocket;
    public GameObject projectile;
    public Transform tip;
    public LayerMask layermask;
    public Transform cam;
    public bool buttonhold;
    public float timebeforeshot;
    public float firerate;
    public float spread;
    public int shotsper = 1;
    public float damage;
    public float speed;
    public bool canshoot = true;
    public float rotatedis;
    public Quaternion tipstartrotation;
    public bool shooting;
    public bool shotfinished;
    public bool dontreturnpos;
    public AudioSource sound;
    // Start is called before the first frame update
    settingsmenu menu;
    float startvol;
    void Start()
    {
        
        menu = GameObject.FindGameObjectWithTag("menu").GetComponent<settingsmenu>();
        if(tipstartrotation == null)
        {
            tipstartrotation = new Quaternion();
        }
        startvol = sound.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (!menu.balls)
        {
            sound.volume = startvol*PlayerPrefs.GetFloat("sfvolume", 1);
            if (rocketlauncherdisapear)
            {
                if (canshoot) rocket.gameObject.SetActive(true);
                else rocket.gameObject.SetActive(false);
            }
            RaycastHit hit;

            if (Physics.Raycast(cam.position, cam.forward, out hit, Mathf.Infinity, layermask))
            {
                if (Vector3.Distance(cam.position, hit.point) > rotatedis) tip.LookAt(hit.point);
                else if (!dontreturnpos) tip.localRotation = tipstartrotation;
            }
            else tip.localRotation = tipstartrotation;
            if (shooting)
            {
                if (canshoot)
                {
                    canshoot = false;
                    Invoke("Shoot", timebeforeshot);
                }

            }
        }
    }
    void Shoot()
    {
        sound.Play();
        for (int i = 0; i < shotsper; i++)
        {
            Quaternion rot = Quaternion.Euler(tip.rotation.eulerAngles + new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), 0f));
            GameObject proj = Instantiate(projectile, tip.position, rot);
            proj.GetComponent<Rigidbody>().velocity = proj.transform.forward * speed;
            proj.GetComponent<projectile>().damage = damage;
            proj.GetComponent<projectile>().fromenemy = true;
        }
        Invoke("canshoottrue", firerate);
        
    }
    void canshoottrue()
    {
        canshoot = true;
        shooting = false;
    }
}
