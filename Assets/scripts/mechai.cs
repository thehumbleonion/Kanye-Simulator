using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mechai : MonoBehaviour
{
    public GameObject deadbody;
    public UnityEngine.AI.NavMeshAgent agent;
    public GameObject detectzone;
    public float nogozone;
    public float jumpzone;
    public float jumpminzone;
    public float recomendedzone;
    public float rotationspeed;
    public GameObject player;
    public string debugstring;
    public Animator anim;
    public float jumpheight;
    public float jumptime;
    public float jumpspeed;
    public GameObject hittingground;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    float starttime;
    float lasthp;
    bool jumping;
    public bool smashing;
    Vector3 centrepoint;
    Vector3 startrelcentre;
    Vector3 endrelcentre;
    Vector3 playerpoint;
    Vector3 startpoint;
    Vector3 lastpos;
    public float balls;
    void Update()
    {
        
        float hp = this.GetComponent<healthparts>().hp;
        if (hp <= 0)
        {
            Instantiate(deadbody, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }

        float playerdistance = Vector3.Distance(transform.position, player.transform.position);
        if (detectzone == null)
        {
            
            
            if (!jumping && !smashing)
            {
                float speed = (lastpos - transform.position).magnitude;
                if (speed*Time.deltaTime > 0f)
                {
                    anim.Play("mech walk");
                }
                else
                {
                    anim.Play("mech idle");
                }
                agent.enabled = true;
                if (playerdistance > recomendedzone)
                {
                    if (playerdistance < jumpzone && playerdistance > jumpminzone)
                    {
                        jump();
                    }
                    else
                    {
                        runningtoplayer();
                    }

                }
                else
                {
                    if (playerdistance > nogozone)
                    {
                        anim.Play("mech attack");
                    }
                    else
                    {
                        runningfromplayer();
                    }
                }
            }
            else if (jumping)
            {
                anim.Play("mech jump");
                agent.enabled = false;
                Vector3 LookPos = player.transform.position - transform.position;
                LookPos.y = 0;
                Quaternion Rotation = Quaternion.LookRotation(LookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, rotationspeed * Time.deltaTime);
                GetCentre(Vector3.up);
                float fraccomplete = (Time.time - starttime) / jumptime * jumpspeed;
                transform.position = Vector3.Slerp(startrelcentre, endrelcentre, fraccomplete * jumpspeed);
                transform.position += centrepoint;
                if (Vector3.Distance(transform.position, playerpoint) < 5f)
                {
                    Instantiate(hittingground, transform.position, transform.rotation);
                    jumping = false;
                }
            }
            else if (smashing)
            {
                attackingplayer();
            }
        }
        else
        {
            anim.Play("mech idle");
        }
        lasthp = this.GetComponent<healthparts>().hp;
        lastpos = transform.position;
    }
    void stunned()
    {

        debugstring = "stunned";
        agent.SetDestination(transform.position);
        GetComponent<enemyweapon>().shooting = false;
    }

    void jump()
    {
        starttime = Time.time;
        jumping = true;
        playerpoint = player.transform.position;
        startpoint = transform.position;
    }

    void attackingplayer()
    {
        debugstring = "attaking player";
        Vector3 lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationspeed * Time.deltaTime);
        agent.SetDestination(transform.position);
    }
    void runningfromplayer()
    {

        Vector3 dirToPlayer = transform.position - player.transform.position;
        Vector3 des = transform.position + dirToPlayer;
        UnityEngine.AI.NavMeshPath navMeshPath = new UnityEngine.AI.NavMeshPath();
        if (agent.CalculatePath(des, navMeshPath) && navMeshPath.status == UnityEngine.AI.NavMeshPathStatus.PathComplete)
        {
            debugstring = "running from player";
            agent.SetDestination(des);
        }
        else
        {
            attackingplayer();
        }
    }
    void runningtoplayer()
    {

        agent.SetDestination(player.transform.position);
        debugstring = "running to player";
    }

    public void GetCentre(Vector3 direction)
    {
        centrepoint = (startpoint + playerpoint) * .5f;
        centrepoint -= direction;
        startrelcentre = startpoint - centrepoint;
        endrelcentre = playerpoint - centrepoint;
    }
}
