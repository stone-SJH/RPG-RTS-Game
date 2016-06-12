using UnityEngine;
using System.Collections;

public class EdgeCheck : MonoBehaviour {
	public float delta = 0.5f;
	public float sxedge1 = 300f;
	public float sxedge2 = 800f;
	public float szedge1 = 0f;
	public float szedge2 = 500f;
	private float xedge1;
	private float xedge2;
	private float zedge1;
	private float zedge2;
	// Use this for initialization
	void Start () {
		xedge1 = sxedge1 + delta;
		xedge2 = sxedge2 - delta;
		zedge1 = szedge1 + delta;
		zedge2 = szedge2 - delta;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.position.x <= xedge1)
			this.transform.position = new Vector3 (xedge1, this.transform.position.y, this.transform.position.z);
		if (this.transform.position.x >= xedge2)
			this.transform.position = new Vector3 (xedge2, this.transform.position.y, this.transform.position.z);
		if (this.transform.position.z <= zedge1)
			this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, zedge1);
		if (this.transform.position.z >= zedge2)
			this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, zedge2);
	}
}
