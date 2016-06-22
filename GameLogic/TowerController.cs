using UnityEngine;
using System.Collections;

public class TowerController : MonoBehaviour {

	public Hero hero;
	
	private bool isOpen;
	private float radius;

	public GameObject[] towers;

	// Use this for initialization
	void Start () {
		radius = 15f;
		isOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isOpen && (Vector3.Distance (hero.transform.position, this.transform.position) <= radius)) {
			this.transform.Rotate (0, 180, 0);
			isOpen = true;

			foreach (GameObject go in towers){
				go.GetComponent<teslaBullet>().ordCD = 99999f;
				go.transform.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/iceShader");
			}
		}
	}
}
