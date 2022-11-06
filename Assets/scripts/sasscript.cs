using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sasscript : MonoBehaviour
{
    public GameObject player;
    public GameObject detectzone;
    public GameObject guns;
    public GameObject[] firepoints;
    public float timebetweenshots;
    public float timelookingatplayer;
    public float time;
    public GameObject projectile;
    public LayerMask raymask;
    public GameObject deadbody;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public bool canseejet;
    void Update()
    {
        if(GetComponent<health>().hp <= 0)
        {
            Instantiate(deadbody, transform.position, transform.rotation);
            Destroy(this.gameObject);

        }
        transform.LookAt(player.transform.position);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        guns.transform.LookAt(player.transform.position);
        Vector3 fromPosition = transform.position;
        Vector3 toPosition = player.transform.position;
        Vector3 direction = toPosition - fromPosition;
        RaycastHit hit;
        canseejet = false;
        if (Physics.Raycast(transform.position, direction, out hit,raymask))
        {
            if (hit.transform.tag == "jet")
            {
                canseejet = true;
            }
        }
        if (canseejet) time += Time.deltaTime;
        else time -= Time.deltaTime;
        if (time < 0) time = 0;
        if (time >= timelookingatplayer)
        {
            int ranint = Random.Range(0, firepoints.Length - 1);
            Instantiate(projectile,firepoints[ranint].transform.position, firepoints[ranint].transform.rotation);
            time = 0;
        }
    }
}
