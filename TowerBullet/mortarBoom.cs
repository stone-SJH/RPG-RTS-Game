using UnityEngine;
using System.Collections;

public class mortarBoom : MonoBehaviour {

    public float Damage;
    private ArrayList targets = new ArrayList();
    private GameObject target;

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
            if (!troop.isDead)
                troop.HP -= Damage;
        }
        else if (hero != null)
        {
            if (hero.HP >= 0)
                hero.HP -= Damage;
        }
    }
}
