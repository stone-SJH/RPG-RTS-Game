using UnityEngine;
using System.Collections;

public class soldier : MonoBehaviour {
    private int speed;

	// Use this for initialization
	void Start () {
        speed = 50;
	}
	
	// Update is called once per frame
	void Update () {
        move();
	
	}

    void move()
    {
        this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("1");
       
    }
}
