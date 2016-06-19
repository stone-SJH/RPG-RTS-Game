using UnityEngine;
using System.Collections;

public class GameModeSwitch : MonoBehaviour {

	public bool RPGmode = false;
	public bool RTSmode = false;

	public GameObject Uicamera;
	public GameObject RPGCamera;
	public GameObject RTSCamera;
	public GameObject RPGMinimapTextureCamera;
	public GameObject RTSMinimapTextureCamera;
	public GameObject RPGHeroPointer;

    public GameObject heroColumn;
    public GameObject heroSkill;
    public GameObject heroHP;
	public GameObject enemyHP;

    public GameObject troopsButton;

	// Use this for initialization
	void Start () {
		//SwitchToRTS ();
		SwitchToRPG ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SwitchToRTS(){
		Uicamera.active = false;
		RTSmode = true;
		RPGmode = false;
		RPGCamera.active = false;
		RTSCamera.active = true;
		RPGMinimapTextureCamera.active = false;
		RTSMinimapTextureCamera.active = true;
		RPGHeroPointer.active = false;
		Uicamera.active = true;

        heroColumn.active = false;
        heroSkill.active = false;
        heroHP.active = false;
        troopsButton.active = true;
		enemyHP.active = true;
	}
	public void SwitchToRPG(){
		RTSmode = false;
		RPGmode = true;
		RPGCamera.active = true;
		RTSCamera.active = false;
		RPGMinimapTextureCamera.active = true;
		RTSMinimapTextureCamera.active = false;
		RPGHeroPointer.active = true;

        heroColumn.active = true;
        heroSkill.active = true;
        heroHP.active = true;
		troopsButton.active = false;
		enemyHP.active = false;
    }
	public void SwitchToNULL(){
		RTSmode = false;
		RPGmode = false;
		RPGCamera.active = false;
		RTSCamera.active = false;
		RPGMinimapTextureCamera.active = true;
		RTSMinimapTextureCamera.active = false;
		RPGHeroPointer.active = true;
		/*
		heroColumn.active = false;
		heroSkill.active = false;
		heroHP.active = false;
		troopsButton.active = false;
		enemyHP.active = false;
		*/
	}
}
