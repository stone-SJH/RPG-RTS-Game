using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class crossbowBulletMove : MonoBehaviour {
    private float x;
    private float y;
    private float z;
    public int speed=500;
    private GameObject target;
    private float Damage;
	private float height;
    private Troop troop;
    private Hero hero;



    void Awake()
    {
        Destroy(this.gameObject, 5);
    }

	// Use this for initialization
	void Start () {
        x = this.GetComponent<Transform>().localEulerAngles.x;
        y = this.GetComponent<Transform>().localEulerAngles.y;
        z = this.GetComponent<Transform>().localEulerAngles.z;
        findTarget();
    }
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			//this.transform.position=new Vector3(this.transform.position)
			this.transform.rotation = Quaternion.LookRotation (new Vector3 (target.transform.position.x, target.transform.position.y + height, target.transform.position.z) - this.transform.position);
			this.transform.Rotate (new Vector3 (90, 0, 0));
        
			this.transform.Translate (Vector3.up * Time.deltaTime * speed);
		} else
			destroyself ();
	}

    void OnTriggerEnter(Collider col)
    {
        //Debug.Log(target.gameObject.name+"1");
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
        target = this.transform.parent.transform.GetComponent<crossbowBullet>().target;
        Damage = this.transform.parent.transform.GetComponent<crossbowBullet>().Damage;
		troop = target.transform.GetComponent<Troop>();
		hero = target.transform.GetComponent<Hero>();
		height = target.transform.GetComponent<CharacterController>().height * target.transform.lossyScale.y * 0.6f;	
    }

    void makeDamage()
    {
		if (troop != null && !troop.isOPState)
            troop.HP -= Damage;
        else if (hero != null && !hero.isOP && !hero.isOPState)
            hero.HP -= Damage;
    }

}
