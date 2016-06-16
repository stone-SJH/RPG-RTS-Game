using UnityEngine;
using System.Collections;

public class RTSMapController : MonoBehaviour {

	public float speed = 50f;
	public float boardOffset = 20f;

	public float xMinBoundary = 150f;
	public float xMaxBoundary = 350f;
	public float zMinBoundary = 370f;
	public float zMaxBoundary = 730f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.mousePosition.x <= (0 + boardOffset)) {
			if (this.transform.position.z >= xMinBoundary)
				this.transform.Translate (-1 * speed * Time.deltaTime, 0, 0);
		}
		if (Input.mousePosition.x >= (Screen.width - boardOffset)) {
			if (this.transform.position.z <= xMaxBoundary)
				this.transform.Translate (1 * speed * Time.deltaTime, 0, 0);
		}
		if (Input.mousePosition.y <= (0 + boardOffset)) {
			if (this.transform.position.x <= zMaxBoundary)
				this.transform.Translate (0, -1 * speed * Time.deltaTime, 0);
		}	
		if (Input.mousePosition.y >= (Screen.height - boardOffset)) {
			if (this.transform.position.x >= zMinBoundary)
				this.transform.Translate (0, 1 * speed * Time.deltaTime, 0);
		}
	}
}
