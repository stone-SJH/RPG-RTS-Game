using UnityEngine;
using System.Collections;

public class RouteManager : MonoBehaviour {
    public int No;
	public PathNode startPN;
	public RouteNode[] start = new RouteNode[10000];
	public RouteNode cur;
	public PathNode curPN;

	public PathNode[] InsidePath;
	// Use this for initialization
	void Start () {
        No = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CreateNewRoute(){
		start[No] = new RouteNode();
		start[No].position = startPN.transform.position;
		start[No].prev = null;
		cur = start[No];
		curPN = startPN;
	}

	public void AddToRoute(PathNode pn){
		if (curPN.ID == 1 && pn.ID == 2) {
			for(int i = 0; i < InsidePath.Length; i++){
				RouteNode _rn = new RouteNode();
				_rn.position = InsidePath[i].transform.position;
				_rn.setPrev(cur);
				cur = _rn;
			}
		}
		RouteNode rn = new RouteNode();
		rn.position = pn.transform.position;
		rn.setPrev(cur);
		cur = rn;
		curPN = pn;
	}

	public void FinishRoute(){
		cur.succ = null;
        No++;
	}
}
