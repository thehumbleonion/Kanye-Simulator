using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlaneMovement : MonoBehaviour
{
	public LayerMask groundraymask;
	public GameObject player;
	public bool forcein;
	public bool In;
	public float grav;
	public float enterrange = 7;
	//[SyncVar]
	public float throttle;
	//[SyncVar]
	public float throttlespeed;
	//[SyncVar]
	public float maxSpeed;
	//[SyncVar]
	public float rot1;
	//[SyncVar]
	public float rot2;
	//[SyncVar]
	public float rotatspeedcontroll;
	public Rigidbody rb;
	//[SyncVar]
	public float speedrot1;
	//[SyncVar]
	public float speedrot2;
	public GameObject camob;
	public GameObject frontob;
	public GameObject backob;
	public GameObject leftwingob;
	public GameObject rightwingob;
	public float speed;
	public float rbspeed;
	public Vector3 vel;
	public int ground;
	public GameObject sensor;
	public GameObject sensor2;
	public GameObject leftwingi;
	public GameObject leftwingo;
	public GameObject rightwingi;
	public GameObject rightwingo;
	public GameObject leftru;
	public GameObject leftrd;
	public GameObject rightru;
	public GameObject rightrd;
	public GameObject deadbody;
	public Slider thrustslider;
	public GameObject ccam;
	public AudioSource beep;
	public float onesecbeepdis;
	public GameObject arrow;
	public GameObject controls;
	public GameObject getin;

    // Start is called before the first frame update
    void Start()
    {
        
    }
	
	// Update is called once per frame
	float beeptime = 0;
	void Forcein()
    {
		player.SetActive(false);
		In = true;
	}
    void FixedUpdate()
    {
        if (forcein)
        {
			Invoke("Forcein", 0.2f);
			forcein = false;
        }
		beeptime += Time.deltaTime;
		GameObject[] missiles = GameObject.FindGameObjectsWithTag("SASMISSILE");
		float closetdist = 500;
		foreach(GameObject mis in missiles)
        {
			float dis = Vector3.Distance(transform.position, mis.transform.position);
			if (dis < closetdist)
            {
				closetdist = dis;
            }
        }
		

		
		if (missiles.Length > 0)
		{
			if (beeptime > closetdist / 500)
			{
				beep.time = 0;
				beeptime = 0;
				float jeff = 0.1f * (closetdist / 500);
				if (jeff < 0)
				{
					jeff = 0;
				}
				beep.pitch = 0.7f + jeff;
				beep.Play();
			}

		}
		GameObject[] sas = GameObject.FindGameObjectsWithTag("sas");
		int closeti = -1;
		int i = 0;
		closetdist = 500000;
		foreach (GameObject m in sas)
		{
			float dis = Vector3.Distance(transform.position, m.transform.position);
			if (dis < closetdist)
			{
				closeti = i;
			}
			i++;
		}
		try { arrow.transform.LookAt(sas[closeti].transform.position); }
		catch {  }
		if (Input.GetKey("c"))
		{
			ccam.SetActive(true);
		}
		else
		{
			ccam.SetActive(false);
		}
		
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			rot1 = 5;
			}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			rot1 = -5;
		}
		else
		{
			rot1 = 0;
		}
		if (Input.GetKey(KeyCode.UpArrow))
		{
			rot2 = 5;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			rot2 = -5;
		}
		else
		{
			rot2 = 0;
		}
		if (In)
		{
			
			getin.SetActive(false);
			controls.SetActive(true);
			player.SetActive(false);
			player.transform.position = transform.position;
			camob.SetActive(true);

			if (Input.GetKey("w"))
			{
				if (throttle < 1)
				{
					throttle += 1f * Time.deltaTime;
				}
			}
			if (Input.GetKey("s"))
			{
				if (throttle > 0)
				{
					throttle -= 1f * Time.deltaTime;
				}
			}
			if (throttle < 0)
			{
				throttle = 0;
			}
			if (throttle > 1)
			{
				throttle = 1;
			}
			thrustslider.value = throttle;
			if (speed < (maxSpeed * throttle))
			{
				speed += (throttle + 1) * throttlespeed * Time.deltaTime;
			}
			else
			{
				speed -= (1 - (throttle)) * throttlespeed * Time.deltaTime;
			}

			if (Physics.Raycast(sensor.transform.position, -transform.up, 1f, groundraymask))
			{
				ground = 1;
			}
			else
			{
				ground = 0;
			}

			leftwingi.transform.localRotation = Quaternion.Euler((-rot1), 0, 0);
			leftwingo.transform.localRotation = Quaternion.Euler((-rot1), -27.337f, 0);
			rightwingi.transform.localRotation = Quaternion.Euler((rot1), 0, 0);
			rightwingo.transform.localRotation = Quaternion.Euler((rot1), 27.337f, 0);
			leftrd.transform.localRotation = Quaternion.Euler((-rot2 * 3), 0, 0);
			rightrd.transform.localRotation = Quaternion.Euler((-rot2 * 3), 0, 0);
			rbspeed = rb.velocity.magnitude;
			speed -= transform.forward.y * Time.deltaTime * 5f;
			vel = Vector3.Lerp(vel, transform.forward, 0.3f);
			rb.velocity = vel * speed * Time.deltaTime * 100;
			if (ground == 0) rb.AddForce(new Vector3(0, -grav * Time.deltaTime * 3000000, 0));
			Vector3 localangularvelocity = transform.InverseTransformVector(rb.angularVelocity);
			localangularvelocity += new Vector3(((rb.velocity.magnitude / 100) * rot2 * Time.deltaTime * rotatspeedcontroll), 0, ((rb.velocity.magnitude / 100) * rot1 * Time.deltaTime * rotatspeedcontroll));
			rb.angularVelocity = transform.TransformVector(localangularvelocity);
			//rb.angularVelocity = new Vector3(0, 0, 0);
			if (Input.GetKeyDown("e"))
			{
				player.SetActive(true);
				In = false;
			}
		}
		else
		{
			controls.SetActive(false);
			camob.SetActive(false);
			if (Vector3.Distance(player.transform.position, transform.position) <= enterrange && !In)
			{
				getin.SetActive(true);
				if (Input.GetKeyDown("e"))
				{
					player.SetActive(false);
					In = true;
				}
			}
			else
			{
				getin.SetActive(false);
			}
		}

		if (GetComponent<health>().hp <= 0)
		{
			die();
		}
		
	}
	void OnCollisionEnter(Collision col)
	{
		if(rbspeed > 100)
		{
			die();
		}
		
	}
	void die()
    {
		player.SetActive(true);
		Instantiate(deadbody, transform.position, transform.rotation);
		Destroy(this.gameObject);
	}
}
