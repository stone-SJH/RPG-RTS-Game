using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scheduleChange : MonoBehaviour {


    public Treasure treasure;

	// Use this for initialization
	void Start () {
        treasure = this.transform.parent.transform.parent.GetComponent<Treasure>();
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Slider>().value = treasure.inOpenTime / treasure.openTime;
	}
}
