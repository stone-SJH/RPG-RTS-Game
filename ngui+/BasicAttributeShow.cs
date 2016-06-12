using UnityEngine;
using System.Collections;

public class BasicAttributeShow : MonoBehaviour {
	public Hero hero;
	public Troop troop001;
	public UILabel label1;
	public UILabel label2;
	public UILabel label3;
	public UILabel label4;
	public UILabel label5;
	public UILabel label6;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		label1.text = hero.HP.ToString ();
		label1.text += "/";
		label1.text += hero.maxHP.ToString ();

		label2.text = hero.attack.ToString ();

		label3.text = hero.speed.ToString ();
		label3.text += "(";
		label3.text += hero.ordSpeed.ToString ();
		label3.text += ")";

		label4.text = troop001.HP.ToString ();
		label4.text += "/";
		label4.text += troop001.maxHP.ToString ();
		
		label5.text = troop001.attack.ToString ();
		
		label6.text = troop001.speed.ToString ();
		label6.text += "(";
		label6.text += troop001.ordSpeed.ToString ();
		label6.text += ")";
	}
}
