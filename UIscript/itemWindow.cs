using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class itemWindow : MonoBehaviour {

    public Hero hero;
    public GameObject hc;
    public Item item;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            hc.GetComponent<herocolumn>().ifuse = true;
            Debug.Log("cancel add or transfer");
        }
	}

    public void close()
    {
        this.transform.GetComponent<Animator>().SetBool("close", true);
        
    }

    void destroyself()
    {
        Destroy(this.gameObject);
    }

    public void beginaddortransfer()
    {
        hc.GetComponent<herocolumn>().ifuse = false;
        hc.GetComponent<herocolumn>().addortransferItem = item;
        Debug.Log("begin add or transfer");
    }

    public void sellItem()
    {

    }

}
