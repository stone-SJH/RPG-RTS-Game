using UnityEngine;
using System.Collections;

public class Troop : MonoBehaviour {
	public string heroName = "Hero";
	public float deadTime = 1f;
	//造价
	public int crystalCost = 200;
	//当前值
	public float HP;
	public float speed;
	public float attack;
	//正常属性值
	public float maxHP = 2000f;
	public float ordSpeed = 8f;
	public float ordAttack = 50f;
	//属性光环
	public bool isInHP = false;
	//public bool isOutHp = true;
	public float nowAddHP = 0f;
	public bool isInSpeed = false;
	//public bool isOutSpeed = true;
	public float nowAddSpeed = 0f;
	//治疗
	public int healCount;
	//无敌
	public bool isOPState = false;
	//减速
	public bool isSlowState = false;
	public float slowRatio = 1f;
	public float slowLastTime = 0f;
	public float inSlowLastTime = 0f;
	//动画
	public float deadMaxAnimationSpeed = 1f;
	public AnimationClip deadAnimation;

	public bool isDead = false;
	private Hero hero;
	private float distance;
	private Animation animation;
	// Use this for initialization
	void Start () {
		GameObject h = GameObject.Find (heroName);
		hero = h.GetComponent<Hero> ();
		animation = GetComponent<Animation>();

		HP = maxHP;
		speed = ordSpeed;
		attack = ordAttack;
		healCount = hero.healCount;
		nowAddHP = hero.addHP;
		nowAddSpeed = hero.addSpeed;
	}

	void SpeedDetermine(){
		speed = ordSpeed;
		if (this.transform.position.y < 5f)
			speed *= 0.5f;
		if (isSlowState)
			speed *= slowRatio;
	}

	void SlowStateCheck(){
		if (isSlowState) {
			inSlowLastTime += Time.deltaTime;
		}
		if (inSlowLastTime >= slowLastTime) {
			isSlowState = false;
			inSlowLastTime = 0f;
			slowLastTime = 0f;
		}
	}

	void BUFFStateCheck(){
		//HP BUFF
		if (nowAddHP != hero.addHP) {
			if (isInHP){
				float p = HP / maxHP;
				maxHP += hero.addHP;
				maxHP -= nowAddHP;
				HP = Mathf.Floor(maxHP * p);
			}
			nowAddHP = hero.addHP;
		}
		if (distance <= hero.addHPRadius && !isInHP) {
			float p = HP / maxHP;
			maxHP += nowAddHP;
			HP = Mathf.Floor(maxHP * p);
			isInHP = true;
		} 
		if (distance > hero.addHPRadius && isInHP) {
			float p = HP / maxHP;
			maxHP -= nowAddHP;
			HP = Mathf.Floor(maxHP * p);
			isInHP = false;
		}
		//Speed BUFF
		if (nowAddSpeed != hero.addSpeed) {
			if (isInSpeed) {
				ordSpeed += hero.addSpeed;
				ordSpeed -= nowAddSpeed;
			}
			nowAddSpeed = hero.addSpeed;
		}
		if (distance <= hero.addSpeedRadius && !isInSpeed) {
			ordSpeed += hero.addSpeed;
			isInSpeed =true;
		}
		if (distance > hero.addSpeedRadius && isInSpeed) {
			ordSpeed -= hero.addSpeed;
			isInSpeed = false;
		}
		//群体治疗BUFF
		if (healCount < hero.healCount) {
			if (distance <= hero.healRadius){
				HP += hero.heal;
				if (HP >= maxHP)
					HP = maxHP;
			}
			healCount++;
		}
		//无敌BUFF
		if (distance <= hero.OPRadius && hero.isOPState) {
			isOPState = true;
		} else
			isOPState = false;

	}

	void DeathCheck(){
		if (HP <= 0 && !isDead) {
			animation.CrossFade (deadAnimation.name);
			isDead = true;
			this.transform.GetComponent<CharacterController>().enabled = false;
			Invoke ("SinkAndDestroy", deadTime);
		}
	}

	void SinkAndDestroy(){
		//Debug.Log("dead done");
		StartCoroutine (sad ());
	}

	IEnumerator sad(){
		while (gameObject.transform.position.y >= -10) {
			gameObject.transform.Translate (0, -2 * Time.deltaTime, 0);
			yield return null;
		}
		Destroy (gameObject);
		yield return null;
	}
	// Update is called once per frame
	void Update () {
		//HP -= 40 * Time.deltaTime;
		SpeedDetermine ();
		SlowStateCheck ();
		DeathCheck ();
		distance = Vector3.Distance (this.transform.position, hero.transform.position);
		BUFFStateCheck ();
	}

	public void GetSlowed(float ratio, float lastTime){
		isSlowState = true;
		slowRatio = ratio;
		slowLastTime = lastTime;
		inSlowLastTime = 0f;
	}
}
