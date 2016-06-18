using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class noticeWindow : MonoBehaviour {

    public Treasure treasure;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void close()
    {
        this.GetComponent<Animator>().SetBool("close", true);
    }

    void destroyself()
    {
        Destroy(this.gameObject);
    }

    public void confirmTreasure()
    {
        treasure.startOpen();
        this.GetComponent<Animator>().SetBool("close", true);
    }

}
