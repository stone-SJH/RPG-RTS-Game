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
	}
	public void SwitchToRPG(){
		RTSmode = false;
		RPGmode = true;
		RPGCamera.active = true;
		RTSCamera.active = false;
		RPGMinimapTextureCamera.active = true;
		RTSMinimapTextureCamera.active = false;
		RPGHeroPointer.active = true;
	}
	public void SwitchToNULL(){
		RTSmode = false;
		RPGmode = false;
		RPGCamera.active = false;
		RTSCamera.active = false;
		RPGMinimapTextureCamera.active = true;
		RTSMinimapTextureCamera.active = false;
		RPGHeroPointer.active = true;
	}
}
