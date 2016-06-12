using UnityEngine;
using System.Collections;

public class TextureCameraMove : MonoBehaviour {
	public GameObject Hero;
	public float Height = 80f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (Hero.transform.position.x, Hero.transform.position.y + Height, Hero.transform.position.z);
	}
}
