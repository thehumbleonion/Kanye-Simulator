using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodproj : MonoBehaviour
{
    public float force;
    public GameObject[] bloodsplats;
    public float[] sizemults;
    public LayerMask wlayer;
    Vector3 origin;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        GetComponent<Rigidbody>().AddForce(transform.forward * force);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = transform.position - origin;
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, 0.2f, wlayer))
        {
            GameObject splat = Instantiate(bloodsplats[Random.Range(0, bloodsplats.Length)],hit.point, Quaternion.LookRotation(hit.normal));
            //splat.transform.localEulerAngles = new Vector3(90, 0, Random.Range(0, 360));
            splat.transform.position += splat.transform.forward * 0.001f;
            float scaler = Random.Range(sizemults[0], sizemults[1]);
            splat.transform.localScale = new Vector3(transform.localScale.x*scaler, transform.localScale.z * scaler, 1);
            Destroy(this.gameObject);

        }
        origin = transform.position;
    }
}
