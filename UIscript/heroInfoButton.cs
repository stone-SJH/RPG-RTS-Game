using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class heroInfoButton : MonoBehaviour {

    public GameObject hi;
    private GameObject myhi;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void makehi()
    {
        myhi = Instantiate(hi);
        myhi.transform.parent = GameObject.Find("Canvas").transform;
        myhi.transform.localPosition = new Vector3(0, 0, 0);
        Hero hero = GameObject.Find("Hero").GetComponent<Hero>();
        myhi.transform.FindChild("Image").transform.FindChild("HP").GetComponent<Text>().text ="HP: "+ hero.HP.ToString()+"/"+hero.maxHP.ToString();
        myhi.transform.FindChild("Image").transform.FindChild("Attack").GetComponent<Text>().text = "攻击力: " + hero.attack.ToString();
        myhi.transform.FindChild("Image").transform.FindChild("Speed").GetComponent<Text>().text = "速度: " + hero.speed.ToString() + "(" + "正常速度: " + hero.ordSpeed + ")";
    }

}
