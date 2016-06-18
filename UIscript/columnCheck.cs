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

    // Use this for initialization
    void Start () {
        string[] numbers = this.gameObject.name.Split(new char[6] { 'c', 'o', 'l', 'u', 'm' ,'n'}, StringSplitOptions.RemoveEmptyEntries);
        number = int.Parse(numbers[0]);
        mycdimg = this.transform.FindChild("cdImg").gameObject;
        imglengthx = mycdimg.transform.GetComponent<RectTransform>().sizeDelta.x;
        imglengthy = mycdimg.transform.GetComponent<RectTransform>().sizeDelta.y;
        mycdimg.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(imglengthx, 0);
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

    }

    public void OnPointerExit(PointerEventData eventData)

    {

    }

}
