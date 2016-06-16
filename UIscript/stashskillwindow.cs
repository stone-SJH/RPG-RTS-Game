using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class stashskillwindow : MonoBehaviour {

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

    void destroyself()
    {
        Destroy(this.gameObject);
    }

}
