using UnityEngine;
using System.Collections;

public class exitButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ok()
    {
        Application.Quit();
    }

    public void cancel()
    {
        this.gameObject.active = false;
    }

}
