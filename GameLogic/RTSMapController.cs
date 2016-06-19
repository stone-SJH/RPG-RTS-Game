using UnityEngine;
using System.Collections;

public class RTSMapController : MonoBehaviour {

	public float speed = 50f;
	public float boardOffset = 20f;

	public float xMinBoundary = 150f;
	public float xMaxBoundary = 350f;
	public float zMinBoundary = 370f;
	public float zMaxBoundary = 730f;

	public float yMinBoundary = 80f;
	public float yMaxBoundary = 150f;
	public float sensitivity = 10f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetAxis ("Mouse ScrollWheel") * sensitivity > 0 && this.transform.position.y <= yMaxBoundary) || (Input.GetAxis ("Mouse ScrollWheel") * sensitivity < 0 && this.transform.position.y >= yMinBoundary))
			this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + Input.GetAxis ("Mouse ScrollWheel") * sensitivity, this.transform.position.z);

		if (Input.mousePosition.x <= (0 + boardOffset)) {
			if (this.transform.position.z >= xMinBoundary)
				this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - speed * Time.deltaTime);
		}
		if (Input.mousePosition.x >= (Screen.width - boardOffset)) {
			if (this.transform.position.z <= xMaxBoundary)
				this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + speed * Time.deltaTime);
		}
		if (Input.mousePosition.y <= (0 + boardOffset)) {
			if (this.transform.position.x <= zMaxBoundary)
				this.transform.position = new Vector3(this.transform.position.x + speed * Time.deltaTime, this.transform.position.y, this.transform.position.z);
		}	
		if (Input.mousePosition.y >= (Screen.height - boardOffset)) {
			if (this.transform.position.x >= zMinBoundary)
				this.transform.position = new Vector3(this.transform.position.x - speed * Time.deltaTime, this.transform.position.y, this.transform.position.z);
		}
	}
}
