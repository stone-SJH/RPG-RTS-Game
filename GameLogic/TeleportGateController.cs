using UnityEngine;
using System.Collections;

public class TeleportGateController : MonoBehaviour {
	public Transform teleport0;
	public Transform teleport1;
	//是否是单向传送门，单向即只从teleport0->teleport1
	public bool single = true;
	public float radius0 = 5f;
	public float radius1 = 5f;

	private Vector3 target0;
	private Vector3 target1;
	// Use this for initialization
	void Start () {
		target0 = teleport0.position;
		target1 = teleport1.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
