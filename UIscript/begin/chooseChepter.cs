using UnityEngine;
using System.Collections;

public class chooseChepter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void chapter1()
    {
		Application.LoadLevel("level1_start");
    }

    public void chapter2()
    {
		Application.LoadLevel("level2_start");
    }

    public void chapter3()
    {
		Application.LoadLevel("level3_start");
    }

    public void chapter4()
    {

        Application.LoadLevel("level4_start");
    }

}
