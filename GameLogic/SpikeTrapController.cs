using UnityEngine;
using System.Collections;

public class SpikeTrapController : MonoBehaviour {
	public float topHeight;
	public float bottomHeight;
	public float upSpeed = 20f;
	public float downSpeed = -2f;
	public float damage = 300f;

	private bool upState = true;
	private bool downState = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.position.y >= topHeight) {
			downState = true;
			upState = false;
		}
		if (this.transform.position.y <= bottomHeight) {
			upState = true;
			downState = false;
		}
		if (upState)
			this.transform.Translate (0, upSpeed * Time.deltaTime, 0);
		if (downState)
			this.transform.Translate (0, downSpeed * Time.deltaTime, 0);
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log (other.gameObject.name);
		Hero hero = other.gameObject.GetComponent<Hero> ();
		hero.HP -= damage;
		this.transform.position = new Vector3 (this.transform.position.x, bottomHeight, this.transform.position.z);
	}

}
