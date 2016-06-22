using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class beginMenu : MonoBehaviour {

    private GameObject objChoose;
    private GameObject objExit;

	// Use this for initialization
	void Start () {
        objChoose = this.transform.FindChild("chooseChapter").transform.FindChild("Image").gameObject;
        objChoose.active = false;
        objExit = this.transform.FindChild("exit").transform.FindChild("Image").gameObject;
        objExit.active = false;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void begin()
    {
        Application.LoadLevel("level1_start");
    }

    public void choose()
    {
        objChoose.active = true;
        objExit.active = false;
    }

    public void option()
    {
        objChoose.active = false;
        objExit.active = false;
    }

    public void exit()
    {
        objChoose.active = false;
        objExit.active = true;
    }

}
