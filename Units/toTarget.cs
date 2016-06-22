using UnityEngine;
using System.Collections;

public class toTarget : MonoBehaviour {
	public GameModeSwitch gms;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if(col!=null && col.gameObject.tag == "soldier")
        {
            if (col.gameObject.GetComponent<Troop>() != null)
            {
                this.GetComponent<TargetBuilding>().HP -= col.gameObject.GetComponent<Troop>().attack;
				col.transform.position = new Vector3(col.transform.position.x, col.transform.position.y - 12, col.transform.position.z);
				col.gameObject.GetComponent<Troop>().HP = 0;
            }
            if (col.gameObject.GetComponent<Hero>() != null)
            {
                this.GetComponent<TargetBuilding>().HP -= col.gameObject.GetComponent<Hero>().attack;
				col.transform.position = new Vector3(col.transform.position.x, col.transform.position.y - 12, col.transform.position.z);
				col.gameObject.GetComponent<Hero>().HP = 0;
				col.gameObject.GetComponent<Hero>().toRTS = false;
				gms.SwitchToRTS ();
            }
        }
    }

}
