using UnityEngine;
using System.Collections;

public class Skill8ButtonController : MonoBehaviour {

	public Hero hero;
	public UILabel label;
	
	private float cd;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (hero.Skill8State () < 0)
			cd = 0f;
		else
			cd = hero.Skill8State ();
		
		label.text = cd.ToString ();
	}

	void OnClick(){
		if (hero.Skill8State() == -1)
			hero.UseSkill8 ();
	}
}
