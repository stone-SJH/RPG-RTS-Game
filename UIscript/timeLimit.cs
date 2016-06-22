using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class timeLimit : MonoBehaviour {

    public GameObject gll;

	// Use this for initialization
	void Start () {
        gll = GameObject.Find("LevelLogicManager").gameObject;
	}
	
	// Update is called once per frame
	void Update () {

        this.GetComponent<Slider>().value = (gll.GetComponent<GameLevelLogic>().levelTime - gll.GetComponent<GameLevelLogic>().gameTime) / gll.GetComponent<GameLevelLogic>().levelTime;

    }
}
