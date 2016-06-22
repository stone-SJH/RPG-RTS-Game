using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class crossbowBullet : MonoBehaviour {
	
    public GameObject bullet;
    private GameObject myBullet;
    private ArrayList targets=new ArrayList();
    public GameObject target;
    private int targetindex;
    private int rotationSpeed=10;
    private Vector3 tt_p;
    private bool isFire;
	public float ordCD = 0.5f; 
    public float CD;
    private float incd;
    public float Damage = 2f;

    private Troop troop;
    private Hero hero;
	private Hero hero2;
	public AudioClip fireSound;

    // Use this for initialization
    void Start () {
        isFire = false;
        incd = 0f;
		hero2 = GameObject.Find ("Hero").GetComponent<Hero> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (hero2.isSlowedState && Vector3.Distance(this.transform.position, hero2.slowedCenter) <= hero2.slowedRadius) {
			CD = ordCD / hero2.slowedRatio;
		} 
		else
			CD = ordCD;
		
		if (hero2.isSlowedState && Vector3.Distance (this.transform.position, hero2.slowedCenter) <= hero2.slowedRadius) {
			this.transform.GetComponent<Animator> ().speed = hero2.slowedRatio;
		} else {
			this.transform.GetComponent<Animator> ().speed = 1f;
		}

        if (isFire)
        {
			if (target != null){
				Fire();
				toTarget();
			}
        }
    }

    void makeBullet()
    {
		this.GetComponent<AudioSource> ().PlayOneShot (fireSound, 0.7f);
        myBullet = Instantiate(bullet);
        myBullet.GetComponent<Transform>().SetParent(this.GetComponent<Transform>());
        myBullet.GetComponent<Transform>().localPosition = new Vector3(0,1,0);
        myBullet.GetComponent<Transform>().rotation = Quaternion.LookRotation(tt_p - myBullet.transform.position);
        myBullet.GetComponent<Transform>().Rotate(new Vector3(90, 0, 0));

        //myBullet.GetComponent<Transform>().Rotate(new Vector3(this.GetComponent<Transform>().FindChild("Base").GetComponent<Transform>().FindChild("Turret").GetComponent<Transform>().localEulerAngles.x, this.GetComponent<Transform>().localEulerAngles.y, 0.0f));
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "soldier") return;
        //Debug.Log("fire");
        targets.Add(col.gameObject);
        if (isFire == false)
        {
            target = (GameObject)targets[0];
            targetindex = 0;
            beginFire();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag != "soldier") return;
		//if (target == null) {

		//}

        if (targets.Count == 1)
        {
            targets.Remove(col.gameObject);
            endFire();
        }
        else if (col.gameObject.name == target.gameObject.name)
        {
            targets.Remove(col.gameObject);
			if (targets.Count > 0)
            	target = (GameObject)targets[0];
        }
        else
        {
            targets.Remove(col.gameObject);
        }
    }

    void OnTriggerStay(Collider col) {

    }


    void deleteBullet()
    {

    }

    void toTarget()
    {
		if (!ifTargetisDead ()) {
			Vector3 t_position = target.transform.position;
			tt_p = t_position;
			Vector3 c_position = this.transform.position;
			Vector3 t_position_1 = t_position;
			Vector3 c_position_1 = c_position;
			t_position.y = c_position.y;
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (t_position - c_position), rotationSpeed * Time.deltaTime);
			this.transform.FindChild ("Base").transform.FindChild ("Turret").rotation =
            Quaternion.Slerp (this.transform.FindChild ("Base").transform.FindChild ("Turret").rotation, Quaternion.LookRotation (t_position_1 - c_position_1), rotationSpeed * Time.deltaTime);
		}
	}
    void changeTarget()
    {

    }

    void beginFire()
    {
        //Debug.Log("fff");
        this.transform.GetComponent<Animator>().SetBool("shoot", true);
        isFire = true;
    }

    void endFire()
    {
        this.transform.GetComponent<Animator>().SetBool("shoot", false);
        isFire = false;
    }

    void Fire()
    {
		if(targets.Count==0){
			endFire();
			return;
		}
		ArrayList clean = new ArrayList ();
		foreach (GameObject obj in targets) {

			if(obj==null){
				clean.Add(obj);
				continue;
			}
			troop = obj.transform.GetComponent<Troop> ();
			hero = obj.transform.GetComponent<Hero> ();
			if(troop!=null){
				if(obj!=target && troop.isDead)
					clean.Add(obj);
			}
			else if(hero!=null){
				if(hero.HP<=0){
					clean.Add(obj);
				}
			}

		}
		foreach (GameObject obj in clean) {
			targets.Remove(obj);
		}
		if (ifTargetisDead())
		{
			if(targets.Count==0){
				endFire();
			}
			else if (targets.Count == 1)
			{
				targets.Remove(target);
				endFire();
			}
			else
			{
				targets.Remove(target);
				target = (GameObject)targets[0];
				//makeBullet();
			}
		}


        if (incd == 0)
        {
     
            makeBullet();
     
            incd += Time.deltaTime;
        }
        else if (incd >= CD)
        {
            incd = 0;
        }
        else
        {
            incd += Time.deltaTime;
        }
    }

    bool ifTargetisDead()
	{
		if (target.gameObject == null) {
			return true;
		}
		troop = target.transform.GetComponent<Troop> ();
		hero = target.transform.GetComponent<Hero> ();
		if ((hero != null && hero.HP <= 0) || (troop != null && troop.isDead))
		{
			
			return true;
		}
		return false;
	}

}
