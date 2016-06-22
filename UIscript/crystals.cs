using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class crystals : MonoBehaviour {

    public Hero hero;

    void Awake()
    {
        Invoke("findHero", 0.5f);
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.FindChild("number").GetComponent<Text>().text = hero.crystals.ToString();
	}

    void findHero()
    {
        hero = GameObject.Find("Hero").GetComponent<Hero>();
    }


}
