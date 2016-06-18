using UnityEngine;
using System.Collections;

public class PathNode : MonoBehaviour {

	public int ID;//点过ID=2的pathnode后自动结束路径
	public PathNode[] adjacent;
	public int state;//1 表示为当前选择点，2表示可作为下一个选择点，0表示不可以作为下一个选择点

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
