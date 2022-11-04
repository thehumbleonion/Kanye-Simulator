using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
	public GameObject dead;
	public Mesh mesh;
	public GameObject exaustbar;
	public Text slidertext;
	public Text healthtext;
	public Slider healthslider;
	public Slider maxhealthslider;
	public Slider stamslider;
	public Slider maxstamslider;
	public CharacterController controller;
	public GameObject camera;
	public float runstamrate;
	public float jumpstaminause;
	public float wallstaminause;
	public float stamina;
	public float staminaregenrate;
	public float maxstamina;
	public float speed;
	public float worldlayer;
	public float wallboxsize;
	public float jumpSpeed;
	public float slidejumpspeed;
	public float gravity;
	public float wallgravity;
	public float Shiftmult;
	private Vector3 moveDirection = Vector3.zero;
	public CapsuleCollider capcol;
	public float contrlsize;
	public float contrlcamy;
	public float controlleroff;
	float startcapsize;
	float camstarty;
	public Vector3 forwd;
	public float maxhealth;
	private void Start()
    {
		GameObject.FindGameObjectWithTag("music").GetComponent<musicmanager>().intensity = 0;
		if(SceneManager.GetActiveScene().name != "levelup") PlayerPrefs.SetInt("levelup", 0);
		forwd = transform.forward;
		startcapsize = capcol.height;
		camstarty = camera.transform.localPosition.y;
		Savenload(true,false,"huh",false,0);
    }
	float shift = 1;
	bool exausted;
	Image image;
	bool lastgrounded;
	public void Savenload(bool load, bool newlevel, string levelname, bool loadscene, int levelprogress)
    {
		string path = Application.dataPath + $"/save{PlayerPrefs.GetInt("savenum",0)}.txt";
		Debug.Log(path);
		items itms = GetComponent<items>();
		if (!File.Exists(path))
        {
			File.WriteAllText(path, "maxhealth=100\nmaxstamina=100\nunlockedguns=True,False,False,False,False\nlastlevel=lv1\nlevelprogress=0");
        }
		string[] lines = File.ReadAllLines(path);
		if (load)
        {
			
			maxhealth = int.Parse(lines[0].Remove(0,10));
			maxstamina = int.Parse(lines[1].Remove(0, 11));
			string[] unlockedguns = lines[2].Remove(0, 13).Split(char.Parse(","));
			int i = 0;
			foreach(string s in unlockedguns)
            {
				if (s == "True")
                {
					itms.unlockedguns[i] = true;
                }
				i++;
            }
		}
		string level = lines[3].Remove(0,10);
		int lvlprog = int.Parse(lines[4].Remove(0, 14));
		if (newlevel)
        {
			level = levelname;
			if (int.Parse(lines[4].Remove(0, 14)) < levelprogress)
			{
				PlayerPrefs.SetInt("levelup", 1);
				lvlprog = levelprogress;
			}
		}
		Debug.Log(itms.unlockedguns[1]);
		File.WriteAllText(path, $"maxhealth={maxhealth}\nmaxstamina={maxstamina}\nunlockedguns={itms.unlockedguns[0]},{itms.unlockedguns[1]},{itms.unlockedguns[2]},{itms.unlockedguns[3]},{itms.unlockedguns[4]}\nlastlevel={level}\nlevelprogress={lvlprog}");
		if (loadscene) SceneManager.LoadScene("loadscene");
	}
	void Update()
	{
		bool canregen = true;
		if (exausted)
		{
			slidertext.text = "E X A U S T E D";
			exaustbar.SetActive(true);
			image = exaustbar.GetComponent<Image>();
			Color tempColor = image.color;
			tempColor.a = (100 - stamina) / 100;
			image.color = tempColor;
		}
		else
		{
			slidertext.text = $"{Mathf.RoundToInt(stamina)}";
			exaustbar.SetActive(false);
		}
		
		stamslider.value = stamina;
		maxstamslider.value = 300-maxstamina;
		if(stamina <= 0)
        {
			exausted = true;
        }
		float lstam = stamina;
        if (stamina >= 100)
        {
			exausted = false;
			if (stamina >= maxstamina) stamina = maxstamina;
        }
		float hp = GetComponent<health>().hp;
		if(hp <= 0)
        {
			Instantiate(dead, transform.position, transform.rotation);
			foreach(Transform t in transform)
            {
				Destroy(t.gameObject);
            }
			try
			{
				GameObject.FindGameObjectWithTag("music").GetComponent<musicmanager>().intensity = 0;
			}
			catch { }
			Destroy(GetComponent<CharacterController>());
			Destroy(GetComponent<CapsuleCollider>());
			Destroy(GetComponent<items>());
			Destroy(GetComponent<FPSDisplay>());
			Destroy(this);
        }
		if (hp > maxhealth)
        {
			GetComponent<health>().hp = maxhealth;
			hp = maxhealth;
        }
		healthtext.text = $"{Mathf.RoundToInt(hp)}";
		healthslider.value = hp;
		maxhealthslider.value = 300 - maxhealth;
		RaycastHit hit;
		bool controllergrounded = controller.isGrounded;
		if (controllergrounded) shift = 1; 
		bool sliding = false;
		if (!exausted && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftControl)))
		{
			if (controllergrounded) shift = Shiftmult;
			if(Input.GetKey(KeyCode.LeftControl)) sliding = true;
		}
        if (sliding)
        {
			camera.transform.localPosition = new Vector3(0, contrlcamy, 0);
			capcol.height = contrlsize;
			controller.center = new Vector3(0, controlleroff, 0);
			capcol.center = new Vector3(0, controlleroff, 0);
		}
        else
        {
			forwd = transform.forward;
			camera.transform.localPosition = new Vector3(0, camstarty, 0);
			capcol.height = startcapsize;
			controller.center = new Vector3(0, 0, 0);
			capcol.center = new Vector3(0, 0, 0);
		}
		float grav = gravity;
		Collider[] cols = Physics.OverlapSphere(transform.position + capcol.center, wallboxsize);
		foreach(Collider obj in cols)
        {
			if (obj.gameObject.layer == worldlayer)
            {
				grav = wallgravity;
				if(!controllergrounded) canregen = false;
				controllergrounded = true;
            }
        }
		Vector3 fw = forwd * Input.GetAxis("Vertical") * speed * shift;
		Vector3 sw = transform.right * Input.GetAxis("Horizontal") * speed;
		Vector3 xy = new Vector3();
		if (sliding) xy = forwd * 1 * speed * shift; else xy = fw + sw;
		moveDirection.x = xy.x;
		moveDirection.z = xy.z;
		if (controller.isGrounded) moveDirection.y = -9;
		float jumps = jumpSpeed;
		if (sliding) jumps = slidejumpspeed;
		bool goingdown = false;
		float gravvy = moveDirection.y - (grav * Time.deltaTime);
		if (gravvy < 0)
		{
			goingdown = true;
		}
		if (controllergrounded && Input.GetButtonDown("Jump") && !exausted)
		{
			if (grav == wallgravity)
			{
				if (goingdown) { moveDirection.y = jumps; stamina -= wallstaminause; }
			}
			else { moveDirection.y = jumps; stamina -= jumpstaminause; }
		}
		if (exausted) grav = gravity;
		
		if (grav != wallgravity) moveDirection.y -= grav * Time.deltaTime;
        else
        {
            if (goingdown)
            {
				moveDirection.y -= grav * Time.deltaTime;
			}
            else
            {
				moveDirection.y -= gravity * Time.deltaTime;
			}
		}
		controller.Move(moveDirection * Time.deltaTime);
		controller.height = capcol.height;
		if(Mathf.Abs(moveDirection.z + moveDirection.x) > 0 && controllergrounded && !sliding && shift > 1)
        {
			stamina -= runstamrate * Time.deltaTime;
        }
		if (lstam > stamina) ; else if(canregen) stamina += staminaregenrate * Time.deltaTime;

		lastgrounded = controller.isGrounded;

	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position + capcol.center, wallboxsize);
		Gizmos.color = Color.green;
		Gizmos.DrawWireMesh(mesh, 0, transform.position + capcol.center, transform.rotation, new Vector3(transform.localScale.x, capcol.height / 2, transform.localScale.z));
	}
}
