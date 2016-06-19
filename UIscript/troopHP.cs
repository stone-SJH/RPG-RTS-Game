using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class troopHP : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.GetComponent<Slider>().value = this.transform.parent.transform.parent.GetComponent<Troop>().HP / this.transform.parent.transform.parent.GetComponent<Troop>().maxHP;

        
    }
}
