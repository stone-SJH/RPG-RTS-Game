using UnityEngine;
using System.Collections;

public class makeSoldier : MonoBehaviour {
    public GameObject soldier;
    private GameObject mysoldier;
    static public ArrayList soldiers=new ArrayList();
    private int number;

	// Use this for initialization
	void Start () {
        number = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("a"))
        {
            mysoldier = Instantiate(soldier);
            mysoldier.gameObject.name = "soldier" + (number++).ToString();
            
            mysoldier.transform.position = this.transform.position;
            mysoldier.transform.Rotate(new Vector3(0, -90, 0));
            soldiers.Add(mysoldier.gameObject);
        }
	
	}
}
