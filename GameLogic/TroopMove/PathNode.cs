using UnityEngine;
using System.Collections;

public class PathNode : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public PathNode prev;
    public PathNode succ;
    public void setSucc(PathNode node)
    {
        if (node == null)
        {
            succ = null;
            return;
        }
        succ = node;
        node.prev = this;
    }
    public void setPrev(PathNode node)
    {
        if (node == null)
        {
            prev = null;
            return;
        }
        prev = node;
        node.succ = this;
    }
    //draw icon
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(this.transform.position, "flag.png");
    }
}
