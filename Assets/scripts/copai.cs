using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class copai : MonoBehaviour
{
    public GameObject deadbody;
    public NavMeshAgent agent;
    public GameObject detectzone;
    public float interupted;
    public float nogozone;
    public float recomendedzone;
    public float rotationspeed;
    public GameObject player;
    public string debugstring;
    public GameObject gunpivot;
    public GameObject gunstarttarget;
    public GameObject guntarget;
    public Animator anim;
    public bool riot;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(interrupt());
        player = GameObject.FindGameObjectWithTag("Player");
    }
    IEnumerator interrupt()
    {
        while (true)
        {
            interupted -= 1;
            yield return new WaitForSeconds(0.1f);
        }
    }
    // Update is called once per frame
    float lasthp;
    void Update()
    {
        anim.SetFloat("vel x", transform.InverseTransformDirection(agent.velocity).x);
        anim.SetFloat("vel z", transform.InverseTransformDirection(agent.velocity).z);
        Vector3 lookPos = guntarget.transform.position - gunpivot.transform.position;
        
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        gunpivot.transform.rotation = Quaternion.Slerp(gunpivot.transform.rotation, rotation, rotationspeed * Time.deltaTime);
        if (riot)gunpivot.transform.localEulerAngles = new Vector3(gunpivot.transform.localEulerAngles.x, 0, gunpivot.transform.localEulerAngles.z);
        float hp = this.GetComponent<health>().hp;
        if(hp <= 0)
        {
            Instantiate(deadbody, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        if (hp < lasthp)
        {
            interupted = (lasthp - hp)/10;
        }
        float playerdistance = Vector3.Distance(transform.position, player.transform.position);
        if (detectzone == null)
        {
            if(interupted <= 0)
            {
                if (playerdistance > recomendedzone)
                {
                    runningtoplayer();
                }
                else
                {
                    if (playerdistance > nogozone)
                    {
                        shootingplayer();
                    }
                    else
                    {
                        runningfromplayer();
                    }
                }
            }
            else
            {
                stunned();
            }
        }
        lasthp = this.GetComponent<health>().hp;
    }
    void stunned()
    {
        guntarget = gunstarttarget;
        debugstring = "stunned";
        agent.SetDestination(transform.position);
        GetComponent<enemyweapon>().shooting = false;
    }
    void shootingplayer()
    {
        debugstring = "shooting player";
        GetComponent<enemyweapon>().shooting = true;
        Vector3 lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationspeed*Time.deltaTime);
        guntarget = player;
        agent.SetDestination(transform.position);
    }
    void runningfromplayer()
    {
        guntarget = gunstarttarget;
        GetComponent<enemyweapon>().shooting = false;
        Vector3 dirToPlayer = transform.position - player.transform.position;
        Vector3 des = transform.position + dirToPlayer;
        NavMeshPath navMeshPath = new NavMeshPath();
        if (agent.CalculatePath(des, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            debugstring = "running from player";
            agent.SetDestination(des);
        }
        else
        {
            shootingplayer();
        }
    }
    void runningtoplayer()
    {
        
        NavMeshPath navMeshPath = new NavMeshPath();
        if (agent.CalculatePath(player.transform.position, navMeshPath) && navMeshPath.status != NavMeshPathStatus.PathComplete)
        {
            shootingplayer();
        }
        else
        {
            guntarget = gunstarttarget;
            GetComponent<enemyweapon>().shooting = false;
            agent.SetDestination(player.transform.position);
            debugstring = "running to player";
        }
    }
}
