using UnityEngine;
using System.Collections;

public class Skill6ButtonController : MonoBehaviour {

	public Hero hero;
	public UILabel label;

	private float cd;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (hero.Skill6State () < 0)
			cd = 0f;
		else
			cd = hero.Skill6State ();

		label.text = cd.ToString ();
	}

	void OnClick(){
		Debug.Log("click");
		if (hero.Skill6State() == -1)
			hero.UseSkill6 ();
	}
}
