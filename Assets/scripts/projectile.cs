using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public bool fromenemy;
    public Rigidbody rb;
    public float timealive;
    public float maxtimealive;
    public bool hit;
    public float damage;
    public float boxsize;
    public LayerMask layerMask;
    public float raycastdis = 0.1f;
    public bool follower;
    public GameObject visible;
    public GameObject followed;
    public float followspeed;
    public float speed;
    public bool boxcoldetection;
    public bool dontdestroy;
    public bool rightclickdestroy;
    public GameObject weapon;
    public bool continuousforce;
    public float forcestrength;
    public bool playerprojectile;
    float ricochecount;
    float startingvel = 1.0f;
    public TrailRenderer trail;
    // Start is called before the first frame update
    GameObject music;
    private void Start()
    {
        Invoke("destroy", maxtimealive);
        
    }
    void balls(Collider other)
    {

        if (hit == false)
        {
            rb.useGravity = true;
            if (other.GetComponent<health>() != null)
            {
                if(other.GetComponent<health>().hitprefab != null) Instantiate(other.GetComponent<health>().hitprefab, transform.position,transform.rotation);
                if (startingvel > 2)other.GetComponent<health>().hp -= damage * (rb.velocity.magnitude/startingvel);
                else other.GetComponent<health>().hp -= damage;
                int i = 0;
                foreach(GradientAlphaKey key in trail.colorGradient.alphaKeys)
                {
                    trail.colorGradient.alphaKeys[i].alpha = (rb.velocity.magnitude / startingvel);
                    i++;
                }
                GameObject.FindGameObjectWithTag("music").GetComponent<musicmanager>().intensity += damage;
            }
            if (!playerprojectile) hit = true;
            Invoke("destroy", timealive);
            ricochecount++;
        }
    }
    bool firstframe = true;
    void Update()
    {
        if (continuousforce)
        {
            rb.velocity = (transform.forward * forcestrength) * Mathf.Pow(0.8f, (ricochecount + 1));
            if (firstframe)
            {
                startingvel = rb.velocity.magnitude;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && rightclickdestroy && weapon.activeSelf)
        {
            destroy();
        }
        if (!follower)
        {
            
            if(!fromenemy)visible.SetActive(false);
            if (!boxcoldetection)
            {
                RaycastHit rhit;
                if (Physics.Raycast(transform.position, transform.forward, out rhit, raycastdis))
                {
                    balls(rhit.collider);
                }
            }
            else if (GetComponent<explosive>() == null)
            {
                Collider[] cols = Physics.OverlapBox(transform.position, new Vector3(boxsize, boxsize, boxsize));
                foreach(Collider cum in cols)
                {
                    if(cum.GetComponent<health>() != null && cum.GetComponent<explosive>() == null)
                    {
                        cum.GetComponent<health>().hp -= damage;
                        GameObject.FindGameObjectWithTag("music").GetComponent<musicmanager>().intensity += damage;
                    }
                }
            }
            
        }
        else
        {
            rb.isKinematic = true;
            if(followed == null)
            {
                this.gameObject.SetActive(false);
            }
            transform.position = Vector3.Lerp(transform.position, followed.transform.position, speed);
        }
        firstframe = false;
    }
    void destroy()
    {
        if (!dontdestroy) this.gameObject.SetActive(false);
        else GetComponent<health>().hp = 0;
    }
}
