using UnityEngine;
using System.Collections;

public class FireController : MonoBehaviour {

    public GameObject firePrefab;
    private GameObject fire;
    public GameObject obj;
	// Use this for initialization
	void Start () {
        fire = GameObject.Find("Fire");
	}

    // Update is called once per frame
    void Update() {
        //实例一个prefab
        if (Vector3.Distance(transform.position, obj.transform.position) < 6.89) {
            Instantiate(firePrefab, fire.transform.position, transform.rotation);
        }
        //调转方向
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(obj.transform.position - transform.position), 1 * Time.deltaTime);
       
    }
}
