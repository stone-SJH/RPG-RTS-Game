using UnityEngine;
using System.Collections;

public class Skill7ButtonController : MonoBehaviour {

	public Hero hero;
	public UILabel label;

	private float cd;	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (hero.Skill7State () < 0)
			cd = 0f;
		else
			cd = hero.Skill7State ();
		
		label.text = cd.ToString ();
	}

	void OnClick(){
		if (hero.Skill7State() == -1)
			hero.UseSkill7 ();
	}
}
