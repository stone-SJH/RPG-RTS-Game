using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour {

	public Hero hero;
	public GameObject magicShield;

	private bool isOpen;
	private float radius;

	public PathNode pn1;
	public PathNode pn2;

	// Use this for initialization
	void Start () {
		radius = 13f;
		isOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isOpen && (Vector3.Distance (hero.transform.position, this.transform.position) <= radius)) {
			this.transform.Rotate(0, 180, 0);
			magicShield.GetComponent<BoxCollider>().isTrigger = true;
			magicShield.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Refractive");
			isOpen = true;

			PathNode[] newAdj1 = new PathNode[pn1.adjacent.Length + 1];
			for(int i = 0; i < pn1.adjacent.Length; i++)
				newAdj1[i] = pn1.adjacent[i];
			newAdj1[pn1.adjacent.Length] = pn2;	
			pn1.adjacent = newAdj1;

			PathNode[] newAdj2 = new PathNode[pn2.adjacent.Length + 1];
			for(int i = 0; i < pn2.adjacent.Length; i++)
				newAdj2[i] = pn2.adjacent[i];
			newAdj2[pn2.adjacent.Length] = pn1;	
			pn2.adjacent = newAdj2;
		}
	}
}
