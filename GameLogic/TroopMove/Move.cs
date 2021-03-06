﻿using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    public RouteNode cur = null;
    public float speed;
    public Move instance;
	public float gravity = 98f;

	public Troop troop;
	private CollisionFlags collisionFlags;
	public Animation _animation;
	public AnimationClip idleAnimation;
	public AnimationClip runAnimation;
	private float verticalSpeed;
	// Use this for initialization
	void Start () {
        instance = this;
		troop = this.transform.GetComponent<Troop> ();
		_animation = GetComponent<Animation>();
	}


	void RotateAndMove()
	{
		float y = transform.eulerAngles.y;
		this.transform.LookAt (cur.position);
		Vector3 target = this.transform.eulerAngles;
		float succ = Mathf.MoveTowardsAngle (y, target.y, 120 * Time.deltaTime);
		this.transform.eulerAngles = new Vector3 (0, succ, 0);
		Vector3 pos1 = transform.position;
		Vector3 pos2 = cur.position;
		float distance = Vector2.Distance (new Vector2 (pos1.x, pos1.z), new Vector2 (pos2.x, pos2.z));
		float xdistance = pos2.x - pos1.x;
		float zdistance = pos2.z - pos1.z;
		float xratio = xdistance / distance;
		float zratio = zdistance / distance;
		if (distance < 6.0f) {
			if (cur.succ == null) {

			} else
				cur = cur.succ;
		}
		Vector3 movement = new Vector3 (speed * xratio, 0, speed * zratio) + new Vector3 (0, verticalSpeed, 0);
		movement *= Time.deltaTime;
		CharacterController controller = GetComponent<CharacterController> ();
		collisionFlags = controller.Move (movement);
		//transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
		if ((collisionFlags & CollisionFlags.CollidedSides) != 0) {
			controller.Move(new Vector3 (speed * zratio, 0, -speed * xratio) * Time.deltaTime * Random.Range(0, 1));
		}
	}

	bool IsGrounded(){
		return (collisionFlags & CollisionFlags.CollidedBelow) != 0;
	}

	void ApplyGravity(){
		if (IsGrounded ())
			verticalSpeed = 0f;
		else
			verticalSpeed -= gravity * Time.deltaTime;
	}

	// Update is called once per frame
	void Update () {
		if (cur == null && !troop.isDead)
			_animation.Play (idleAnimation.name);
		else if (!troop.isDead)
			_animation.Play (runAnimation.name);
		speed = troop.speed;
		ApplyGravity();
		if (cur != null && !troop.isDead)
			RotateAndMove ();
	}
}
