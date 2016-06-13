using UnityEngine;
using System.Collections;

public class TeleportGateController : MonoBehaviour {
	public Transform teleport0;
	public Transform teleport1;
	//是否是单向传送门，单向即只从teleport0->teleport1
	public bool single = true;
	public float radius0 = 5f;
	public float radius1 = 5f;
	public float coolDown = 5f;	
	public Hero hero;

	private bool canTeleport = true;
	private float inCoolDownTime = 0f;
	private Troop[] troops;
	private Vector3 target0;
	private Vector3 target1;
	// Use this for initialization
	void Start () {
		target0 = teleport0.position;
		target1 = teleport1.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (canTeleport && Vector2.Distance (new Vector2 (hero.transform.position.x, hero.transform.position.z), new Vector2 (target0.x, target0.z)) <= radius0) {
			hero.transform.position = target1;
			canTeleport = false;
			inCoolDownTime = 0f;
		}
		if (!single && canTeleport && Vector2.Distance (new Vector2 (hero.transform.position.x, hero.transform.position.z), new Vector2 (target1.x, target1.z)) <= radius1) {
			hero.transform.position = target0;
			canTeleport = false;
			inCoolDownTime = 0f;
		}
		if (!canTeleport) {
			inCoolDownTime += Time.deltaTime;
		}
		if (inCoolDownTime >= coolDown) {
			canTeleport = true;
			inCoolDownTime = 0f;
		}
	}
}
