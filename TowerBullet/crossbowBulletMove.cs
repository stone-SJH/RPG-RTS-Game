using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class crossbowBulletMove : MonoBehaviour {
    private float x;
    private float y;
    private float z;
    public int speed=500;
    private GameObject target;

	// Use this for initialization
	void Start () {
        x = this.GetComponent<Transform>().localEulerAngles.x;
        y = this.GetComponent<Transform>().localEulerAngles.y;
        z = this.GetComponent<Transform>().localEulerAngles.z;
        findTarget();
    }
	
	// Update is called once per frame
	void Update () {
        //this.transform.position=new Vector3(this.transform.position)
        this.transform.rotation = Quaternion.LookRotation(target.transform.position - this.transform.position);
        this.transform.Rotate(new Vector3(90, 0, 0));
        
        this.transform.Translate(Vector3.up * Time.deltaTime*speed);
        

	
	}

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(target.gameObject.name+"1");
        if (col.gameObject.name == target.gameObject.name)
        {
            destroyself();
            makeDamage();
        }
    }

    void destroyself()
    {
        Destroy(this.gameObject);
    }

    void findTarget()
    {
        /*foreach (GameObject obj in makeSoldier.soldiers)
        {
            if(obj.name== this.transform.parent.GetComponent<Text>().text)
            {
                target = obj;
                break;
            }
        }*/
        target = this.transform.parent.transform.GetComponent<crossbowBullet>().target;

    }

    void makeDamage()
    {

    }

}
