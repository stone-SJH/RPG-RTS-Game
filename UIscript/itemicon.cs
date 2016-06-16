using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class itemicon : MonoBehaviour {

    private string itemName;
    private string itemNum;
    public GameObject itemInfo;
    private GameObject myItemInfo;

    private Sprite mysprite;

	// Use this for initialization
	void Start () {
        
        itemName = "hero_1";
        mysprite = Resources.Load<Sprite>(itemName);
        this.GetComponent<Image>().sprite = mysprite;
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void makeItemInfo()
    {
        myItemInfo = Instantiate(itemInfo);
        myItemInfo.transform.SetParent(GameObject.Find("Canvas").GetComponent<Canvas>().transform);
        myItemInfo.transform.localPosition = new Vector3(0,0,0);
        myItemInfo.transform.FindChild("itemInfo").transform.FindChild("itemEdge").transform.FindChild("Image").GetComponent<Image>().sprite = mysprite;
    }

}
