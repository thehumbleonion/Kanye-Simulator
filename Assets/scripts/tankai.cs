using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class tankai : MonoBehaviour
{
    public GameObject player;
    public GameObject deadbody;
    public NavMeshAgent agent;
    public float speed;
    public float mindistance;
    public Vector3 destination;
    public GameObject rotatepoint;
    public GameObject rocketgun;
    public GameObject turret;
    public float turretspeed;
    public Vector2 turretrotaterange;
    void Start()
    {
        
    }

    // Update is called once per frame
    bool moving;
    bool rotated;
    Quaternion targrot;

    void Update()
    {
        float hp = this.GetComponent<healthparts>().hp;
        if (hp <= 0)
        {
            Instantiate(deadbody, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        //turretstuff
        Vector3 lookPos = player.transform.position - rotatepoint.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        rotatepoint.transform.rotation = Quaternion.Slerp(rotatepoint.transform.rotation, rotation, 100 * Time.deltaTime);
        rocketgun.transform.LookAt(player.transform.position);
        lookPos = player.transform.position - turret.transform.position;
        lookPos.y = 0;
        rotation = Quaternion.LookRotation(lookPos);
        turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, rotation, turretspeed * Time.deltaTime);
        if(turret.transform.localEulerAngles.y < turretrotaterange.x)
        {
            //turret.transform.localEulerAngles = new Vector3(0, turretrotaterange.x, 0);
        }
        else if (turret.transform.localEulerAngles.y > turretrotaterange.y)
        {
            
        }
        GetComponent<enemyweapon>().shooting = true;
        turret.GetComponent<enemyweapon>().shooting = true;
        //movement stuff
        if (!moving)
        {
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(transform.position, player.transform.position, NavMesh.AllAreas, path);
            try
            {
                destination = path.corners[1];
            }
            catch
            {
                destination = transform.position;
            }
            moving = true;
            Vector3 relativePos = destination - transform.position;
            targrot = Quaternion.LookRotation(relativePos);
        }
        else
        {
            if (Mathf.RoundToInt(targrot.y/10) != Mathf.RoundToInt(transform.rotation.y/10) && rotated == false)
            {
                transform.rotation = Quaternion.Lerp(this.transform.rotation, targrot, Time.deltaTime * speed);
                agent.SetDestination(transform.position);
            }
            else if (Vector3.Distance(transform.position,player.transform.position) < mindistance || (Mathf.RoundToInt(transform.position.x/10) == Mathf.RoundToInt(destination.x/10) && Mathf.RoundToInt(transform.position.z / 10) == Mathf.RoundToInt(destination.z/10)))
            {
                moving = false;
                rotated = false;
                agent.SetDestination(transform.position);
            }
            else
            {
                agent.SetDestination(destination);
                rotated = true;
            }

        }
        
    }
}
