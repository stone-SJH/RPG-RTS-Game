using UnityEngine;
using System.Collections;

public class crystalBulletBoom : MonoBehaviour {
    private ArrayList targets = new ArrayList();

    void Awake()
    {
        Destroy(this.gameObject, 2);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "soldier") return;
        targets.Add(col.gameObject);
    }
}
