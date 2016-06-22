using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class columnCheck : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Hero hero;
    public Item item;
    public int number;
    public bool isempty = true;

    private GameObject mycdimg;
    private float imglengthx;
    private float imglengthy;

    public GameObject iteminfo;

    // Use this for initialization
    void Start () {
        string[] numbers = this.gameObject.name.Split(new char[6] { 'c', 'o', 'l', 'u', 'm' ,'n'}, StringSplitOptions.RemoveEmptyEntries);
        number = int.Parse(numbers[0]);
        mycdimg = this.transform.FindChild("cdImg").gameObject;
        imglengthx = mycdimg.transform.GetComponent<RectTransform>().sizeDelta.x;
        imglengthy = mycdimg.transform.GetComponent<RectTransform>().sizeDelta.y;
        mycdimg.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(imglengthx, 0);
        iteminfo = this.transform.FindChild("iteminfo1").gameObject;
        iteminfo.active = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isempty)
        {
            this.transform.FindChild("Text").GetComponent<Text>().text = item.itemNumber.ToString();
            foreach(ItemState itm in hero.itemStates)
            {
                if (itm.ID == item.itemID)
                {
                    if (mycdimg != null && itm.inCD)
                    {
                        mycdimg.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(imglengthx,
                            imglengthy * (itm.CD-itm.inCDTime) / itm.CD);
                        mycdimg.transform.GetComponent<RectTransform>().localPosition = new Vector3(mycdimg.transform.GetComponent<RectTransform>().localPosition.x,
                            -0.5f * imglengthy * (itm.inCDTime) / itm.CD, mycdimg.transform.GetComponent<RectTransform>().localPosition.z);
                    }
                }
            }
        }
        else
        {
            this.transform.FindChild("Text").GetComponent<Text>().text = "";
        }
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        iteminfo.active = true;
        ItemManager im = GameObject.Find("GameLogicManager").GetComponent<ItemManager>();
		ItemCache ic=new ItemCache();
		if (item != null) {
			ic = im.FindInCache (item.itemID);
		
			string[] splitArray2 = ic.Attributes.Split (new char[2] { ';', '_' }, 3, StringSplitOptions.RemoveEmptyEntries);
			string[] splitArray3 = splitArray2 [0].Split (new char[1] { '-' }, 2, StringSplitOptions.RemoveEmptyEntries);
			Debug.Log (splitArray3 [0]);//这个是物品名字
			Debug.Log (splitArray3 [1]);//这个是物品简介
			//myItemInfo.transform.FindChild("itemInfo").transform.FindChild("title").GetComponent<Text>().text = splitArray3[0];
			//myItemInfo.transform.FindChild("itemInfo").transform.FindChild("itemName").GetComponent<Text>().text = splitArray3[1];
			if (splitArray2 [1] == "0") {
				string[] splitArray4 = splitArray2 [2].Split (new char[1] { '_' }, StringSplitOptions.RemoveEmptyEntries);
           
				iteminfo.transform.FindChild ("info").GetComponent<Text> ().text = splitArray3 [0] + "\n" + "物品等级: " + splitArray4 [0] + "\n" +
					"HP: " + splitArray4 [1] + "\n" + "速度: " + splitArray4 [2] + "\n" + "攻击力: " + splitArray4 [3] + "\n" + "价格: " + splitArray4 [4];
			} else if (splitArray2 [1] == "1") {
				string[] splitArray4 = splitArray2 [2].Split (new char[1] { '_' }, StringSplitOptions.RemoveEmptyEntries);
				iteminfo.transform.FindChild ("info").GetComponent<Text> ().text = splitArray3 [0] + "\n" + "回复血量: " + splitArray4 [1] + "  " +
					"提升HP上限: " + splitArray4 [2] + "\n" + "提升速度: " + splitArray4 [3] + "\n" + "持续时间: " + splitArray4 [4] + "  " + "冷却时间: " + splitArray4 [5] + "\n" +
					"价格: " + splitArray4 [6];

			} else {
				iteminfo.transform.FindChild ("info").GetComponent<Text> ().text = splitArray3 [0] + "\n" + splitArray3 [1];
			}
		}
    }

    public void OnPointerExit(PointerEventData eventData)

    {
        iteminfo.active = false;
    }

}
