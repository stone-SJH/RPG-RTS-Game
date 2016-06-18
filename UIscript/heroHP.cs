using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class heroHP : MonoBehaviour {

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
        if (hero != null)
        {
            this.transform.GetComponent<Slider>().value = hero.HP / hero.maxHP;
        }
	}

    void findHero()
    {
        hero=GameObject.Find("Hero").GetComponent<Hero>();
    }

}
