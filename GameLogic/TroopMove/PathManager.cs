using UnityEngine;
using System.Collections;

public class PathManager : MonoBehaviour {
	public PathNode[] _pathnodes; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PathnodeAccessableCheck(PathNode cur){
		foreach (PathNode pn in _pathnodes) {
			pn.state = 0;
		}
		cur.state = 1;
		foreach (PathNode pn2 in cur.adjacent) {
			pn2.state = 2;
		}
	}
}
