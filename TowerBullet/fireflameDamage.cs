using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class fireflameDamage : MonoBehaviour{

    public GameObject bullet;
    private GameObject myBullet;
    private ArrayList targets = new ArrayList();
    public GameObject target;
    private int targetindex;
    private int rotationSpeed = 10;
    private Vector3 tt_p;
    private bool isFire;
	public float ordCD = 0.5f;
	private float CD;
    private float incd;

    public float Damage = 30f;

    private Troop troop;
    private Hero hero;
	private Hero hero2;

	public AudioClip fireSound;

    // Use this for initialization
    void Start()
    {
        isFire = false;
        incd = 0;
		hero2 = GameObject.Find ("Hero").GetComponent<Hero> ();
    }

    // Update is called once per frame
    void Update()
    {
		if (hero2.isSlowedState && Vector3.Distance(this.transform.position, hero2.slowedCenter) <= hero2.slowedRadius) {
			CD = ordCD / hero2.slowedRatio;
		} 
		else
			CD = ordCD;
        //toTarget();

		if (hero2.isSlowedState && Vector3.Distance (this.transform.position, hero2.slowedCenter) <= hero2.slowedRadius) {
			this.transform.GetComponent<Animator> ().speed = hero2.slowedRatio;
		} else {
			this.transform.GetComponent<Animator> ().speed = 1f;
		}

		if (isFire)
        {
            //toTarget();
            if (targets.Count == 0)
                endFire();
            else
                Fire();
        }
        

    }

    void makeBullet()
    {
        myBullet = Instantiate(bullet);
        myBullet.GetComponent<Transform>().SetParent(this.GetComponent<Transform>());
        myBullet.GetComponent<Transform>().localPosition = new Vector3(0, 0, 0);
        //myBullet.GetComponent<Transform>().rotation = Quaternion.LookRotation(tt_p - myBullet.transform.position);
        //myBullet.GetComponent<Transform>().Rotate(new Vector3(90, 0, 0));

        //myBullet.GetComponent<Transform>().Rotate(new Vector3(this.GetComponent<Transform>().FindChild("Base").GetComponent<Transform>().FindChild("Turret").GetComponent<Transform>().localEulerAngles.x, this.GetComponent<Transform>().localEulerAngles.y, 0.0f));
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "soldier") return;
        targets.Add(col.gameObject);
        if (isFire == false)
        {
            //target = (GameObject)targets[0];
            //targetindex = 0;
            //this.GetComponent<Text>().text = target.gameObject.name;
            beginFire();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag != "soldier") return;
        if (targets.Count == 1)
        {
            targets.Remove(col.gameObject);
            endFire();
        }
        
        else
        {
            targets.Remove(col.gameObject);
        }
    }

    void OnTriggerStay(Collider col)
    {

    }


    void deleteBullet()
    {
     
        Destroy(myBullet);
    }

    void toTarget()
    {
        Vector3 t_position = target.transform.position;
        tt_p = t_position;
        Vector3 c_position = this.transform.position;
        Vector3 t_position_1 = t_position;
        Vector3 c_position_1 = c_position;
        t_position.y = c_position.y;
        //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(t_position - c_position), rotationSpeed * Time.deltaTime);
        //this.transform.FindChild("Base").transform.FindChild("Turret").rotation =
        //Quaternion.Slerp(this.transform.FindChild("Base").transform.FindChild("Turret").rotation, Quaternion.LookRotation(t_position_1 - c_position_1), rotationSpeed * Time.deltaTime);
    }

    void changeTarget()
    {

    }

    void beginFire()
    {
        this.transform.GetComponent<Animator>().SetBool("shoot", true);
        isFire = true;
        makeBullet();
    }

    void endFire()
    {
        this.transform.GetComponent<Animator>().SetBool("shoot", false);
        isFire = false;
        deleteBullet();
    }

    void Fire()
    {
        if (incd == 0)
        {
            //makeBullet();
            makeDamage();
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

    void makeDamage()
    {
		this.GetComponent<AudioSource> ().PlayOneShot (fireSound, 0.7f);
		ArrayList cleaner = new ArrayList ();
        foreach(GameObject obj in targets)
        {
            troop = obj.transform.GetComponent<Troop>();
            hero = obj.transform.GetComponent<Hero>();
            if (troop != null){
				if(!troop.isDead && !troop.isOPState)
                    troop.HP -= Damage;
                else
					cleaner.Add(obj);
            }
			else if (hero != null){
				if(hero.HP >= 0f && !hero.isOP && !hero.isOPState)
                    hero.HP -= Damage;
                else
					cleaner.Add(obj);
            }
        }
		foreach (GameObject obj in cleaner)
			targets.Remove (obj);
    }

}
