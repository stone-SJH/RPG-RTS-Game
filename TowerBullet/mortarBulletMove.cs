using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mortarBulletMove : MonoBehaviour {

    private float x;
    private float y;
    private float z;
   
    private GameObject target;
    private Vector3 endPosition;
    private Vector3 startPosition;
    public float g = 9.8f;
	private float initSpeed;
	private float horiSpeed;
	private float vertSpeed;
    public GameObject boom;
    private GameObject myboom;

	private bool isBoomed = false;
	private Vector3 targetPosition;
    private float Damage;
	private float height;
    private float dTime = 0;

    

    // Use this for initialization
    void Start()
    {
        x = this.GetComponent<Transform>().localEulerAngles.x;
        y = this.GetComponent<Transform>().localEulerAngles.y;
        z = this.GetComponent<Transform>().localEulerAngles.z;
        findTarget();
		float d = Mathf.Sqrt ((targetPosition.x - startPosition.x) * (targetPosition.x - startPosition.x) + (targetPosition.z - startPosition.z) * (targetPosition.z - startPosition.z));
		float h = Mathf.Abs(targetPosition.y - startPosition.y);
		initSpeed = Mathf.Sqrt(Mathf.Abs((d * d * g * g) / (g * h + g * d)));
		vertSpeed = initSpeed * Mathf.Sqrt (2f) / 2f;
		horiSpeed = initSpeed * Mathf.Sqrt (2f) / 2f;
	}
    
    // Update is called once per frame
    void Update()
    {
		this.transform.LookAt (new Vector3(targetPosition.x, this.transform.position.y, targetPosition.z));
		this.transform.Translate (Vector3.forward * horiSpeed * Time.deltaTime);
		this.transform.Translate (0, vertSpeed * Time.deltaTime, 0);
		vertSpeed -= g * Time.deltaTime;
		if (!isBoomed && this.transform.position.y <= targetPosition.y) {
			Invoke ("Boom", 0);
			isBoomed = true;
		}

    }

    void Boom()
    {
        myboom = Instantiate(boom);
		myboom.transform.position = targetPosition;
        myboom.transform.GetComponent<mortarBoom>().Damage=Damage;
        destroyself();
    }

    void OnTriggerEnter(Collider col)
    {
	}

    void destroyself()
    {
        Destroy(this.gameObject);
    }

    void findTarget()
    {	
        target = this.transform.parent.parent.transform.GetComponent<mortarBullet>().target;
		if (target != null) {
			height = target.transform.GetComponent<CharacterController> ().height * target.transform.lossyScale.y * 0.1f;
			targetPosition = new Vector3 (target.transform.position.x, target.transform.position.y + height, target.transform.position.z);
			startPosition = this.transform.position;
			endPosition = target.transform.position;
			Damage = this.transform.parent.parent.transform.GetComponent<mortarBullet> ().Damage;
			this.transform.parent.DetachChildren ();
		}
    }

    void makeDamage()
    {
        
    }
}
