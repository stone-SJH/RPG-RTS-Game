using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class stashButton : MonoBehaviour {

    public Hero hero;
    public GameObject ssw;
    private GameObject myssw;
    public GameObject itemInfo;
    public GameObject skillInfo;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void makessw()
    {
        myssw = Instantiate(ssw);
        myssw.transform.parent = this.transform.parent.transform.parent;
        myssw.transform.localPosition = new Vector3(0, 20, 0);
        myssw.transform.FindChild("skills").transform.FindChild("skill1").GetComponent<skill1Button>().hero = hero;
        myssw.transform.FindChild("skills").transform.FindChild("skill2").GetComponent<skill2Button>().hero = hero;
        myssw.transform.FindChild("skills").transform.FindChild("skill3").GetComponent<skill3Button>().hero = hero;
        myssw.transform.FindChild("skills").transform.FindChild("skill4").GetComponent<skill4Button>().hero = hero;
        myssw.transform.FindChild("skills").transform.FindChild("skill5").GetComponent<skill5Button>().hero = hero;
        myssw.transform.FindChild("skills").transform.FindChild("skill6").GetComponent<skill6Button>().hero = hero;
        myssw.transform.FindChild("skills").transform.FindChild("skill7").GetComponent<skill7Button>().hero = hero;
        myssw.transform.FindChild("skills").transform.FindChild("skill8").GetComponent<skill8Button>().hero = hero;
        myssw.transform.FindChild("skills").transform.FindChild("skill9").GetComponent<skill9Button>().hero = hero;
        myssw.transform.FindChild("skills").transform.FindChild("skill1").GetComponent<skill1Button>().info = skillInfo;
        myssw.transform.FindChild("skills").transform.FindChild("skill2").GetComponent<skill2Button>().info = skillInfo;
        myssw.transform.FindChild("skills").transform.FindChild("skill3").GetComponent<skill3Button>().info = skillInfo;
        myssw.transform.FindChild("skills").transform.FindChild("skill4").GetComponent<skill4Button>().info = skillInfo;
        myssw.transform.FindChild("skills").transform.FindChild("skill5").GetComponent<skill5Button>().info = skillInfo;
        myssw.transform.FindChild("skills").transform.FindChild("skill6").GetComponent<skill6Button>().info = skillInfo;
        myssw.transform.FindChild("skills").transform.FindChild("skill7").GetComponent<skill7Button>().info = skillInfo;
        myssw.transform.FindChild("skills").transform.FindChild("skill8").GetComponent<skill8Button>().info = skillInfo;
        myssw.transform.FindChild("skills").transform.FindChild("skill9").GetComponent<skill9Button>().info = skillInfo;
        myssw.transform.FindChild("stash").GetComponent<heroStash>().hero = hero;
        myssw.transform.FindChild("stash").GetComponent<heroStash>().itemInfo = itemInfo;
        this.transform.parent.FindChild("heroColumn").GetComponent<herocolumn>().hs = myssw.transform.FindChild("stash").gameObject;
    }

}
