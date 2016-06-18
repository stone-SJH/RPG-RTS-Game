using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;



public class stashCheck : MonoBehaviour {

    
    public int number;
    public bool isempty=true;
    public Item item;

	// Use this for initialization
	void Start () {
        string[] numbers = this.gameObject.name.Split(new char[5] { 's', 't', 'a', 's', 'h' }, StringSplitOptions.RemoveEmptyEntries);
        number = int.Parse(numbers[0]);
        
	}
	
	// Update is called once per frame
	void Update () {
        if (!isempty)
        {
            this.transform.FindChild("Text").GetComponent<Text>().text = item.itemNumber.ToString();
        }
        else
        {
            this.transform.FindChild("Text").GetComponent<Text>().text = "";
        }
    }
	
    void incd()
    {
        if (!isempty)
        {
           
        }
    }
	

}
