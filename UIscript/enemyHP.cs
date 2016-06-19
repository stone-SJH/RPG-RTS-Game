using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class enemyHP : MonoBehaviour {

    public TargetBuilding tb;
    public float maxHP;


	// Use this for initialization

    void Awake()
    {
        Invoke("findTarget", 0.5f);
    }

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (tb != null)
        {
            this.transform.GetComponent<Slider>().value = tb.HP / maxHP;
        }
    }

    void findTarget()
    {
        tb = GameObject.Find("TargetBuilding").GetComponent<TargetBuilding>();
        maxHP = tb.HP;
    }

}
