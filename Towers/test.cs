using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class test : MonoBehaviour {


    public Text tests;
    private int n;
	// Use this for initialization
	void Start () {
        n = 0;
        tests.text = n.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    public void onClick()
    {
        n++;
        tests.text =n.ToString() ;
    }
}
