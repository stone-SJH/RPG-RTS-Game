using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	public int level;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetMouseButtonUp(0)) {
			Application.LoadLevel("Level"+level.ToString());
        }

    }
}
