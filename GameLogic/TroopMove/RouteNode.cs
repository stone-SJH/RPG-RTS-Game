using UnityEngine;
using System.Collections;

public class RouteNode{
	public RouteNode prev = null;
	public RouteNode succ = null;
	public Vector3 position;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setPrev(RouteNode pre){
		pre.succ = this;
		this.prev = pre;
	}
}
