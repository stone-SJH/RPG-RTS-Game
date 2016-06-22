using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class heroStash : MonoBehaviour {

    public Hero hero;
    public GameObject itemInfo;
    private GameObject myItemInfo;

	private GameObject sp;

	// Use this for initialization
	void Start () {
        init();
		sp = this.transform.FindChild ("skillPoint").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        //init();
		sp.GetComponent<Text> ().text = "技能点: \n" + hero.skillPoint.ToString ();
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
        ItemManager im = GameObject.Find("GameLogicManager").GetComponent<ItemManager>();
        ItemCache ic = im.FindInCache(it.itemID);
        string[] splitArray2 = ic.Attributes.Split(new char[2] { ';', '_' }, 3, StringSplitOptions.RemoveEmptyEntries);
        string[] splitArray3 = splitArray2[0].Split(new char[1] { '-' }, 2, StringSplitOptions.RemoveEmptyEntries);
        Debug.Log(splitArray3[0]);//这个是物品名字
        Debug.Log(splitArray3[1]);//这个是物品简介
        myItemInfo.transform.FindChild("itemInfo").transform.FindChild("title").GetComponent<Text>().text = splitArray3[0];
        myItemInfo.transform.FindChild("itemInfo").transform.FindChild("itemName").GetComponent<Text>().text = splitArray3[1];

        //Debug.Log(splitArray2[2]);
        if (splitArray2[1] == "0")
        {
            string[] splitArray4 = splitArray2[2].Split(new char[1] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            myItemInfo.transform.FindChild("itemInfo").transform.FindChild("information").GetComponent<Text>().text = "需要等级: " + splitArray4[0] + "\n" +
                "HP: " + splitArray4[1] + "\n" + "速度: " + splitArray4[2] + "\n" + "攻击力: " + splitArray4[3] + "\n" + "价格: " + splitArray4[4];


        }
        else if (splitArray2[1] == "1")
        {
            string[] splitArray4 = splitArray2[2].Split(new char[1] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            myItemInfo.transform.FindChild("itemInfo").transform.FindChild("information").GetComponent<Text>().text = "回复血量: " + splitArray4[1] + "  " +
                "提升HP上限: " + splitArray4[2] + "\n" + "提升速度: " + splitArray4[3] + "\n" + "持续时间: " + splitArray4[4] + "  " + "冷却时间: " + splitArray4[5] + "\n" +
                "价格: " + splitArray4[6];
              
        }
        else
        {

        }


        //myItemInfo.transform.FindChild("itemInfo").
    }

}
