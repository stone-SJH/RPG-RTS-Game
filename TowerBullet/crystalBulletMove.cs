using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class crystalBulletMove : MonoBehaviour
{

    private float x;
    private float y;
    private float z;
    public int speed=100;
    private GameObject target;
    public GameObject boom;
    private GameObject myboom;

    private float Damage;
	private Hero hero;
	private Troop troop;
	private float height;

    // Use this for initialization
    void Start()
    {
        x = this.GetComponent<Transform>().localEulerAngles.x;
        y = this.GetComponent<Transform>().localEulerAngles.y;
        z = this.GetComponent<Transform>().localEulerAngles.z;
        findTarget();
        

    }

    // Update is called once per frame
    void Update()
    {
		if (target != null) {
			this.transform.rotation = Quaternion.LookRotation (new Vector3 (target.transform.position.x, target.transform.position.y + height, target.transform.position.z) - this.transform.position);
			this.transform.Translate (Vector3.forward * Time.deltaTime * speed);
		} else
			destroyself ();
	}

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(target.gameObject.name + "1");
        if (col.gameObject.name == target.gameObject.name)
        {
            myboom = Instantiate(boom);
            myboom.transform.position = this.transform.position;
            myboom.transform.GetComponent<crystalBulletBoom>().Damage = Damage;
            destroyself();
        }
    }

    void destroyself()
    {
        Destroy(this.gameObject);
    }

    void findTarget()
    {
        target = this.transform.parent.transform.GetComponent<crystalBullet>().target;
        Damage = this.transform.parent.transform.GetComponent<crystalBullet>().Damage;
		troop = target.transform.GetComponent<Troop>();
		hero = target.transform.GetComponent<Hero>();
		height = target.transform.GetComponent<CharacterController>().height * target.transform.lossyScale.y * 0.1f;
    }

    

}
