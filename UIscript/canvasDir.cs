using UnityEngine;
using System.Collections;

public class canvasDir : MonoBehaviour {

    public Camera rpgcam;
    public Camera rtscam;
    private GameModeSwitch gms;

	// Use this for initialization
	void Start () {
        rpgcam = GameObject.Find("RPGCamera").GetComponent<Camera>();
        rtscam = GameObject.Find("RTSCamera").GetComponent<Camera>();
        gms = GameObject.Find("GameLogicManager").GetComponent<GameModeSwitch>();
    }
	
	// Update is called once per frame
	void Update () {
        if (gms.RPGmode)
        {
            this.transform.rotation = rpgcam.transform.rotation;
        }
        else if (gms.RTSmode)
        {
            this.transform.rotation = rtscam.transform.rotation;
        }
	}
}
