using UnityEngine;
using System.Collections;

public class victory : MonoBehaviour {

    public GameObject menu;

	// Use this for initialization
	void Start () {
        menu = this.transform.FindChild("gameMenu").gameObject;
        menu.active = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void appearMenu()
    {
        this.GetComponent<Animator>().SetBool("pause", true);
        menu.active = true;
		Time.timeScale = 0;
    }

}
