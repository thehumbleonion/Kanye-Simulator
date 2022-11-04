using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sniper : MonoBehaviour
{
    public GameObject rotatepoint;
    public GameObject player;
    public GameObject detectzone;
    public float rotationspeed;
    bool shooting;
    public int timeonplayer;    
    public int timeforshoot;
    public bool lookingatplayer;
    public LineRenderer linerenderer;
    public Gradient[] gradients;
    public GameObject deadbody;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(playerlookloop());

    }
    IEnumerator playerlookloop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if(lookingatplayer)timeonplayer += 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        float hp = this.GetComponent<health>().hp;
        if (hp <= 0)
        {
            Instantiate(deadbody, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        if (detectzone == null)
        {
            RaycastHit hit;
            enemyweapon weapon = GetComponent<enemyweapon>();
            if (Physics.Raycast(rotatepoint.transform.position, rotatepoint.transform.forward, out hit))
            {
                if (hit.transform.tag == "Player") lookingatplayer = true;
                else lookingatplayer = false;
            }
            else
            {
                lookingatplayer = false;
            }
            
            if (GetComponent<enemyweapon>().canshoot == true)
            {
                Vector3 lookPos = player.transform.position - transform.position;
                lookPos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationspeed * Time.deltaTime);
                lookPos = player.transform.position - rotatepoint.transform.position;
                rotation = Quaternion.LookRotation(lookPos);
                rotatepoint.transform.rotation = Quaternion.Slerp(rotatepoint.transform.rotation, rotation, rotationspeed * Time.deltaTime);
                if (timeonplayer >= timeforshoot)
                {
                    weapon.shooting = true;
                    timeonplayer = 0;
                }
            }
            if (weapon.shooting) linerenderer.colorGradient = gradients[1];
            else linerenderer.colorGradient = gradients[0];
        }
    }
}
