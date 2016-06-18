using UnityEngine;
using System.Collections;

public class heroInfoWindow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void close()
    {
        this.transform.GetComponent<Animator>().SetBool("close", true);
    }

    public void destroyself()
    {
        Destroy(this.gameObject);
    }
}
