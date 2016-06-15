using UnityEngine;
using System.Collections;

public class crystalBulletBoom : MonoBehaviour {
    private ArrayList targets = new ArrayList();
    private GameObject target;
    public float Damage;
	public float slowRatio = 0.5f;
	public float slowLastTime = 5f;
    private Troop troop;
    private Hero hero;

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
        target = col.gameObject;
        troop = target.transform.GetComponent<Troop>();
        hero = target.transform.GetComponent<Hero>();
        makeDamage();
    }

    void makeDamage()
    {
        if (troop != null)
        {
			if (!troop.isDead && !troop.isOPState){
                troop.HP -= Damage;
				troop.GetSlowed(slowRatio, slowLastTime);
			}
        }
        else if (hero != null)
        {
			if (hero.HP >= 0f && !hero.isOP && !hero.isOPState){
                hero.HP -= Damage;
				hero.GetSlowed(slowRatio, slowLastTime);
			}
        }
        
    }

}
