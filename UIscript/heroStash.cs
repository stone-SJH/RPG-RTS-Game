using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class heroStash : MonoBehaviour {

    public Hero hero;
    public GameObject itemInfo;
    private GameObject myItemInfo;

	// Use this for initialization
	void Start () {
        init();
	}
	
	// Update is called once per frame
	void Update () {
        //init();
	}

    public void init()
    {
        int i;
        //Debug.Log(hero.stash.Count);
        for (i = 0; i < hero.stash.Count; i++)
        {
            Item it = (Item)hero.stash[i];
            int id = it.itemID;
            int num = it.itemNumber;
            GameObject stash = this.transform.FindChild("stash" + (i + 1).ToString()).gameObject;
            stash.transform.GetComponent<stashCheck>().item = it;
            stash.transform.GetComponent<stashCheck>().isempty = false;
            stash.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>(id.ToString());
        }
        for (; i < 40; i++)
        {
            GameObject stash = this.transform.FindChild("stash" + (i + 1).ToString()).gameObject;
            stash.transform.GetComponent<stashCheck>().item = null;
            stash.transform.GetComponent<stashCheck>().isempty = true;
            stash.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("transparent");
        }

    }

    public void clickStashCheck(GameObject obj)
    {
        int num = obj.transform.GetComponent<stashCheck>().number;
        Debug.Log(obj.transform.GetComponent<stashCheck>().isempty);
        if (obj.transform.GetComponent<stashCheck>().isempty)
        {
            

        }
        else
        {
            makeItemInfo(obj.transform.GetComponent<stashCheck>().item);
        }
    }

    public void makeItemInfo(Item it)
    {
        myItemInfo = Instantiate(itemInfo);
        myItemInfo.transform.SetParent(GameObject.Find("Canvas").GetComponent<Canvas>().transform);
        myItemInfo.transform.localPosition = new Vector3(0, 0, 0);
        myItemInfo.transform.GetComponent<itemWindow>().hero = hero;
        myItemInfo.transform.GetComponent<itemWindow>().hc = GameObject.Find("Canvas").transform.FindChild("heroColumn").transform.FindChild("heroColumn").gameObject;
        myItemInfo.transform.FindChild("itemInfo").transform.FindChild("itemEdge").transform.FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(it.itemID.ToString());
        myItemInfo.transform.GetComponent<itemWindow>().item = it;
        GameObject.Find("Canvas").transform.FindChild("heroColumn").transform.FindChild("heroColumn").GetComponent<herocolumn>().iw = myItemInfo.gameObject;
    }

}
