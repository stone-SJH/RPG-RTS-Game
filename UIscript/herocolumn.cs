using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class herocolumn : MonoBehaviour {

    public Hero hero;
    public bool ifuse;
    public Item addortransferItem;
    public GameObject hs;
    public GameObject iw;

    void Awake()
    {
        Invoke("init", 1f);
    }

	// Use this for initialization
	void Start () {
        init();
        ifuse = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void init()
    {
        int i;
        for(i = 0; i < 6; i++)
        {
            Item it = hero.column[i];
            if (hero.column[i] == null) continue;
            int id = it.itemID;
            int num = it.itemNumber;
            GameObject column = this.transform.FindChild("column" + (i + 1).ToString()).gameObject;
            column.transform.GetComponent<columnCheck>().item = it;
            column.transform.GetComponent<columnCheck>().isempty = false;
            column.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>(id.ToString());
            
        }
    }


    public void click(GameObject obj)
    {
        int num = obj.transform.GetComponent<columnCheck>().number;
        
        if (ifuse)
        {
            
            if (obj.transform.GetComponent<columnCheck>().isempty)
            {

                Debug.Log("no Item to use");
            }
            else
            {
                Item it = obj.transform.GetComponent<columnCheck>().item;
                hero.UseItem(it, num - 1);
                Debug.Log("useItem");
            }
        }
        else
        {
            if (obj.transform.GetComponent<columnCheck>().isempty)
            {
                hero.AddToColumn(addortransferItem,num - 1);
                Debug.Log("add Item to column "+ (num-1).ToString());
                if (addortransferItem == null) Debug.Log("item is null");
                ifuse = true;
                obj.transform.GetComponent<columnCheck>().isempty = false;
                init();
                hs.GetComponent<heroStash>().init();
                Destroy(iw.gameObject);
            }
            else
            {
                hero.Transfer(addortransferItem, num - 1);
                Debug.Log("transfer item to " + (num - 1).ToString());
                ifuse = true;
                init();
                hs.GetComponent<heroStash>().init();
                Destroy(iw.gameObject);
            }
        }
    }


}
