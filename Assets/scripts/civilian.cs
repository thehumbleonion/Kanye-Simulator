using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class civilian : MonoBehaviour
{
    public Material[] skincolors;
    public Material[] clothescolors;
    public GameObject[] skin,clothes;
    public GameObject dress;
    public bool hasdress;
    public GameObject detectzone;
    public NavMeshAgent agent;
    public Vector3 target;
    public float wanderradius;
    public GameObject deadbody;
    public Animator anim;
    public float walkspeed;
    public float runspeed;
    // Start is called before the first frame update
    void Start()
    {
        randomize();
        StartCoroutine(targetloop());
    }
    void randomize()
    {
        int skincolor = Random.Range(0, skincolors.Length);
        foreach (GameObject ski in skin)
        {
            ski.GetComponent<SkinnedMeshRenderer>().material = skincolors[skincolor];
        }
        foreach (GameObject clothe in clothes)
        {
            clothe.GetComponent<SkinnedMeshRenderer>().material = clothescolors[Random.Range(0, clothescolors.Length)];
        }
        if (hasdress)
        {
            int nuts = Random.Range(0, 2);
            Debug.Log(nuts);
            if (nuts == 1)
            {
                dress.SetActive(false);
            }
            else
            {
                dress.SetActive(true);
            }
        }
    }
    // Update is called once per frame
    IEnumerator targetloop()
    {
        
        while (true)
        {
            target = RandomNavSphere(transform.position, wanderradius, -1);
            
            yield return new WaitForSeconds(4);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            randomize();
        }
        agent.SetDestination(target);
        if (GetComponent<health>().hp <= 0)
        {
            GameObject dead = Instantiate(deadbody,transform.position,transform.rotation);
            dead.transform.localScale = transform.lossyScale;
            Destroy(this.gameObject);
        }
        if (detectzone == null)
        {
            anim.SetBool("running", true);
            agent.speed = runspeed;
            
        }
        else
        {
            agent.speed = walkspeed;
        }
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
