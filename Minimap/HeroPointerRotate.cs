using UnityEngine;
using System.Collections;

public class HeroPointerRotate : MonoBehaviour {
	public float InitRotationz = -90f;
	public GameObject Hero;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int exp;
		if (Hero.transform.rotation.y > 0)
			exp = 1;
		else
			exp = -1;
		float angle = InitRotationz - Hero.transform.rotation.y * Hero.transform.rotation.y * 180 * exp;
		if (angle > 180)
			angle -= 360;
		if (angle < -180)
			angle += 360;
		float z = Mathf.Sqrt (Mathf.Abs(angle) / 180);
		if (angle > 0)
			exp = 1;
		else
			exp = -1;
		z *= exp;
		while (z >= 1)
			z -= 2;
		while (z <= -1)
			z += 2;
		this.transform.rotation = new Quaternion (this.transform.rotation.x, this.transform.rotation.y, z, this.transform.rotation.w);
	}
}
