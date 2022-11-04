using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemydoor : MonoBehaviour
{
    public GameObject[] people;
    public bool canopen;
    public Color opencolor;
    public SpriteRenderer doorsprite;
    public GameObject detectzone;
    public bool sceneload;
    public string scenename;
    public int levelprogress = 1;
    // Start is called before the first frame update
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if ((canopen && other.tag == "Player") || (canopen && other.tag == "jet"))
        {
            if (sceneload)
            {
                if(other.tag == "jet")
                {
                    Destroy(other.gameObject);
                    player.SetActive(true);
                    player.GetComponent<movement>().Savenload(false, true, scenename, true, levelprogress);
                }
                else
                {
                    player.GetComponent<movement>().Savenload(false, true, scenename, true, levelprogress);
                }
                
                
            }
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(detectzone == null)
        {
            if (canopen == false)
            {
                bool isnull = true;
                foreach (GameObject person in people)
                {
                    if (person != null)
                    {
                        isnull = false;
                    }
                }
                if (isnull)
                {
                    canopen = true;
                }
            }
            else
            {
                doorsprite.color = opencolor;
            }
        }
    }
}
