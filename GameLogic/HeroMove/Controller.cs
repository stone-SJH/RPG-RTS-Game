﻿using UnityEngine;
using System.Collections;


[RequireComponent(typeof(CharacterController))]

public class Controller : MonoBehaviour
{

	public Hero hero;
	public bool isDead;
	public GameModeSwitch gms;
	public Transform upper;
	public Treasure toPickUp;

    public AnimationClip idleAnimation;
    public AnimationClip walkAnimation;
    public AnimationClip runAnimation;
	public AnimationClip bladerunAnimation;
	public AnimationClip drawbladeAnimation;
	public AnimationClip putbladeAnimation;
	public AnimationClip pickupAnimation;
    public AnimationClip jumpPoseAnimation;
	public AnimationClip deadAnimation;

	public bool canMove = true;
	public bool bladeInHand = false;
	public float drawBladeTime = 0f;
	public bool startRun = false;
	public float putBladeTime = 0f;
	public bool startTrot = true;

    public float walkMaxAnimationSpeed = 1.75f;
    public float trotMaxAnimationSpeed = 1.0f;
    public float runMaxAnimationSpeed = 3.0f;
    public float jumpAnimationSpeed = 1.15f;
    public float landAnimationSpeed = 1.0f;

    private Animation _animation;

    enum CharacterState
    {
        Idle = 0,
        Walking = 1,
        Trotting = 2,
        Running = 3,
        Jumping = 4,
    }

    private CharacterState _characterState;

    // The speed when walking
    public float walkSpeed = 5.0f;
    // after trotAfterSeconds of walking we trot with trotSpeed
    public float trotSpeed = 12.0f;
    // when pressing "Fire3" button (cmd) we start running
    public float runSpeed = 30.0f;

    float inAirControlAcceleration = 3.0f;

    // How high do we jump when pressing jump and letting go immediately
    float jumpHeight = 2.0f;

    // The gravity for the character
    float gravity = 98.0f;
    // The gravity in controlled descent mode
    float speedSmoothing = 10.0f;
    float rotateSpeed = 500.0f;
    float trotAfterSeconds = 1.0f;

    bool canJump = true;

    private float jumpRepeatTime = 0.05f;
    private float jumpTimeout = 0.15f;
    private float groundedTimeout = 0.25f;

    // The camera doesnt start following the target immediately but waits for a split second to avoid too much waving around.
    private float lockCameraTimer = 5.0f;

    // The current move direction in x-z
    private Vector3 moveDirection = Vector3.zero;
    // The current vertical speed
    private float verticalSpeed = 0.0f;
    // The current x-z move speed
    private float moveSpeed = 0.0f;

    // The last collision flags returned from controller.Move
    private CollisionFlags collisionFlags;

    // Are we jumping? (Initiated with jump button and not grounded yet)
    private bool jumping = false;
    private bool jumpingReachedApex = false;

    // Are we moving backwards (This locks the camera to not do a 180 degree spin)
    private bool movingBack = false;
    // Is the user pressing any keys?
    private bool isMoving = false;
    // When did the user start walking (Used for going into trot after a while)
    private float walkTimeStart = 0.0f;
    // Last time the jump button was clicked down
    private float lastJumpButtonTime = -10.0f;
    // Last time we performed a jump
    private float lastJumpTime = -1.0f;

    // the height we jumped from (Used to determine for how long to apply extra jump power after jumping.)
    private float lastJumpStartHeight = 0.0f;

    private Vector3 inAirVelocity = Vector3.zero;

    private float lastGroundedTime = 0.0f;

    private bool isControllable = true;

    void Awake()
    {
        moveDirection = transform.TransformDirection(Vector3.forward);
		isDead = false;
        _animation = GetComponent<Animation>();
    }

    void UpdateSmoothedMovementDirection()
    {
        Transform cameraTransform = Camera.main.transform;
        bool grounded = IsGrounded();

        // Forward vector relative to the camera along the x-z plane	
        Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;

        // Right vector relative to the camera
        // Always orthogonal to the forward vector
        Vector3 right = new Vector3(forward.z, 0, -forward.x);

        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // Are we moving backwards or looking backwards
        if (v < -0.2f)
            movingBack = true;
        else
            movingBack = false;

        bool wasMoving = isMoving;
        isMoving = Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f;

        // Target direction relative to the camera
        Vector3 targetDirection = h * right + v * forward;

        // Grounded controls
        if (grounded)
        {
            // Lock camera for short period when transitioning moving & standing still
            lockCameraTimer += Time.deltaTime;
            if (isMoving != wasMoving)
                lockCameraTimer = 0.0f;

            // We store speed and direction seperately,
            // so that when the character stands still we still have a valid forward direction
            // moveDirection is always normalized, and we only update it if there is user input.
            if (targetDirection != Vector3.zero)
            {
                // If we are really slow, just snap to the target direction
                if (moveSpeed < walkSpeed * 0.9f && grounded)
                {
                    moveDirection = targetDirection.normalized;
				}
                // Otherwise smoothly turn towards it
                else
                {
                    moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);

                    moveDirection = moveDirection.normalized;
                }
            }

            // Smooth the speed based on the current target direction
            float curSmooth = speedSmoothing * Time.deltaTime;

            // Choose target speed
            //* We want to support analog input but make sure you cant walk faster diagonally than just forward or sideways
            float targetSpeed = Mathf.Min(targetDirection.magnitude, 1.0f);

            _characterState = CharacterState.Idle;

            // Pick speed modifier
            if (Input.GetKey(KeyCode.LeftShift) | Input.GetKey(KeyCode.RightShift))
            {
                targetSpeed *= runSpeed;
                _characterState = CharacterState.Running;
            }
            else if (Time.time - trotAfterSeconds > walkTimeStart)
            {
                targetSpeed *= trotSpeed;
                _characterState = CharacterState.Trotting;
            }
            else
            {
                targetSpeed *= walkSpeed;
                _characterState = CharacterState.Walking;
            }

            moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, curSmooth);

            // Reset walk time start when we slow down
            if (moveSpeed < walkSpeed * 0.3f)
                walkTimeStart = Time.time;
        }
        // In air controls
        else
        {
            // Lock camera while in air
            if (jumping)
                lockCameraTimer = 0.0f;

            if (isMoving)
                inAirVelocity += targetDirection.normalized * Time.deltaTime * inAirControlAcceleration;
        }

    }

    void ApplyJumping()
    {
        // Prevent jumping too fast after each other
        if (lastJumpTime + jumpRepeatTime > Time.time)
            return;

        if (IsGrounded())
        {
            // Jump
            // - Only when pressing the button down
            // - With a timeout so you can press the button slightly before landing		
            if (canJump && Time.time < lastJumpButtonTime + jumpTimeout)
            {
                verticalSpeed = CalculateJumpVerticalSpeed(jumpHeight);
                SendMessage("DidJump", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    void ApplyGravity()
    {
        if (isControllable)	// don't move player at all if not controllable.
        {
            // Apply gravity
            bool jumpButton = Input.GetButton("Jump");

            // When we reach the apex of the jump we send out a message
            if (jumping && !jumpingReachedApex && verticalSpeed <= 0.0f)
            {
                jumpingReachedApex = true;
                SendMessage("DidJumpReachApex", SendMessageOptions.DontRequireReceiver);
            }

            if (IsGrounded())
                verticalSpeed = 0.0f;
            else
                verticalSpeed -= gravity * Time.deltaTime;
        }
    }

    float CalculateJumpVerticalSpeed(float targetJumpHeight)
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * targetJumpHeight * gravity);
    }

    void DidJump()
    {
        jumping = true;
        jumpingReachedApex = false;
        lastJumpTime = Time.time;
        lastJumpStartHeight = transform.position.y;
        lastJumpButtonTime = -10;

        _characterState = CharacterState.Jumping;
    }

	IEnumerator DrawBlade(){
		startRun = false;
		_animation.Play(drawbladeAnimation.name);
		yield return new WaitForSeconds(_animation[drawbladeAnimation.name].time);
		startRun = true;
	}

    void Update()
    {
		if (hero.HP > 0) {
			if (gms.RPGmode && canMove) {
				walkSpeed = hero.speed * 0.3f;
				if (!startTrot)
					trotSpeed = hero.speed * 0.1f;
				else
					trotSpeed = hero.speed * 0.6f;
				if (!startRun)
					runSpeed = hero.speed * 0.1f;
				else
					runSpeed = hero.speed * 1.0f;
			} else {
				walkSpeed = 0f;
				trotSpeed = 0f;
				runSpeed = 0f;
			}
			if (!isControllable) {
				// kill all inputs if not controllable.
				Input.ResetInputAxes ();
			}

			if (Input.GetButtonDown ("Jump")) {
				lastJumpButtonTime = Time.time;
			}

			if (gms.RPGmode && canMove) {
				UpdateSmoothedMovementDirection ();
			}
			// Apply gravity
			// - extra power jump modifies gravity
			// - controlledDescent mode modifies gravity
			ApplyGravity ();

			// Apply jumping logic
			//ApplyJumping();

			// Calculate actual motion
			Vector3 movement = moveDirection * moveSpeed + new Vector3 (0, verticalSpeed, 0) + inAirVelocity;
			movement *= Time.deltaTime;

			// Move the controller
			CharacterController controller = GetComponent<CharacterController> ();
			collisionFlags = controller.Move (movement);
			// ANIMATION sector
			if (_animation) {
				if (!canMove) {
					_animation.Play (pickupAnimation.name);
				} else if (_characterState == CharacterState.Jumping) {
					if (!jumpingReachedApex) {
						_animation [jumpPoseAnimation.name].speed = jumpAnimationSpeed;
						_animation [jumpPoseAnimation.name].wrapMode = WrapMode.ClampForever;
						_animation.CrossFade (jumpPoseAnimation.name);
					} else {
						_animation [jumpPoseAnimation.name].speed = -landAnimationSpeed;
						_animation [jumpPoseAnimation.name].wrapMode = WrapMode.ClampForever;
						_animation.CrossFade (jumpPoseAnimation.name);
					}
				} else {
					if (controller.velocity.sqrMagnitude < 0.1f) {
						_animation.CrossFade (idleAnimation.name);
					} else {
						if (_characterState == CharacterState.Running) {
							if (!bladeInHand) {
								startRun = false;
								bladeInHand = true;
								_animation.Play (drawbladeAnimation.name);
								drawBladeTime = 0f;
							}
							if (!startRun) {
								drawBladeTime += Time.deltaTime;
							}
							if (drawBladeTime >= 0.5f)
								startRun = true;

							if (startRun) {
								_animation [bladerunAnimation.name].speed = Mathf.Clamp (controller.velocity.magnitude, 0.0f, runMaxAnimationSpeed);
								_animation.CrossFade (bladerunAnimation.name);
							}
						} else if (_characterState == CharacterState.Trotting) {
							if (bladeInHand) {
								startTrot = false;
								bladeInHand = false;
								_animation.CrossFade (putbladeAnimation.name);
								putBladeTime = 0f;
							}
							if (!startTrot)
								putBladeTime += Time.deltaTime;
							if (putBladeTime >= 0.5f)
								startTrot = true;
							if (startTrot) {
								_animation [walkAnimation.name].speed = Mathf.Clamp (controller.velocity.magnitude, 0.0f, trotMaxAnimationSpeed);
								_animation.CrossFade (walkAnimation.name);
							}
						} else if (_characterState == CharacterState.Walking) {
							if (bladeInHand) {
								_animation.CrossFade (putbladeAnimation.name);
								bladeInHand = false;
							}
							_animation [walkAnimation.name].speed = Mathf.Clamp (controller.velocity.magnitude, 0.0f, walkMaxAnimationSpeed);
							_animation.CrossFade (walkAnimation.name);
						}

					}
				}
			}
			// ANIMATION sector

			// Set rotation to the move direction
			if (IsGrounded ()) {

				transform.rotation = Quaternion.LookRotation (moveDirection);

			} else {
				Vector3 xzMove = movement;
				xzMove.y = 0;
				if (xzMove.sqrMagnitude > 0.001f) {
					transform.rotation = Quaternion.LookRotation (xzMove);
				}
			}

			// We are in jump mode but just became grounded
			if (IsGrounded ()) {
				lastGroundedTime = Time.time;
				inAirVelocity = Vector3.zero;
				if (jumping) {
					jumping = false;
					SendMessage ("DidLand", SendMessageOptions.DontRequireReceiver);
				}
			}
		} else {
			if (!isDead && hero.toRTS){
				isDead = true;
				_animation.CrossFade(deadAnimation.name);
				Invoke ("SinkAndDestroy", 3f);
			}
		}
    }

	void SinkAndDestroy(){
		//Debug.Log("dead done");
		gms.SwitchToRTS ();
		StartCoroutine (sad ());
	}
	
	IEnumerator sad(){
		while (gameObject.transform.position.y >= -10f) {
			gameObject.transform.Translate (0, -2 * Time.deltaTime, 0);
			yield return null;
		}
		yield return null;
	}

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //	Debug.DrawRay(hit.point, hit.normal);
        if (hit.moveDirection.y > 0.01f)
            return;
    }

    float GetSpeed()
    {
        return moveSpeed;
    }

    public bool IsJumping()
    {
        return jumping;
    }

    bool IsGrounded()
    {
        return (collisionFlags & CollisionFlags.CollidedBelow) != 0;
    }

    Vector3 GetDirection()
    {
        return moveDirection;
    }

    public bool IsMovingBackwards()
    {
        return movingBack;
    }

    public float GetLockCameraTimer()
    {
        return lockCameraTimer;
    }

    bool IsMoving()
    {
        return Mathf.Abs(Input.GetAxisRaw("Vertical")) + Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f;
    }

    bool HasJumpReachedApex()
    {
        return jumpingReachedApex;
    }

    bool IsGroundedWithTimeout()
    {
        return lastGroundedTime + groundedTimeout > Time.time;
    }

    void Reset()
    {
        gameObject.tag = "Player";
    }

    /*void OnTriggerEnter(Collider e)
    {
        Debug.Log("trigger");
        if (e.gameObject.tag.CompareTo("enemytank") == 0)
        {
            Debug.Log("撞到tank");
            //Destroy(e.gameObject);
        }
        else
        {
            Debug.Log("撞到其他物体");
        }
    }*/
    /*void OnCollisionEnter(Collision e)
    {
        Debug.Log("collision");
        if (e.gameObject.tag.CompareTo("enemytank") == 0)
        {
            Debug.Log("撞到tank");
            //Destroy(e.gameObject);
        }
        else
        {
            Debug.Log("撞到其他物体");
        }
    } */
}