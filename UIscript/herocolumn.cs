﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class herocolumn : MonoBehaviour {

    public Hero hero;
    public bool ifuse;
    public Item addortransferItem;
    public GameObject hs;
    public GameObject iw;

    private GameObject heroname;
    private GameObject herogold;
    private GameObject herolevel;

    public GameObject gameMenu;
    private GameObject mygm;

    //public bool pause;

    void Awake()
    {
        Invoke("init", 1f);
    }

	// Use this for initialization
	void Start () {
        init();
        ifuse = true;
        //pause = false;
        heroname = this.transform.FindChild("heroName").gameObject;
        herogold = this.transform.FindChild("heroGold").gameObject;
        herolevel = this.transform.FindChild("heroLevel").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        heroname.GetComponent<Text>().text = hero.heroName;
        herolevel.GetComponent<Text>().text = hero.level.ToString();
        herogold.GetComponent<Text>().text = hero.golds.ToString();

        
	}

    void init()
    {
        int i;
        for(i = 0; i < 6; i++)
        {
            Item it = hero.column[i];
            GameObject column = this.transform.FindChild("column" + (i + 1).ToString()).gameObject;
            if (hero.column[i] == null)
            {
                column.transform.GetComponent<columnCheck>().item = null;
                column.transform.GetComponent<columnCheck>().isempty = true;
                column.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("transparent");
                continue;
            }
            int id = it.itemID;
            int num = it.itemNumber;
           
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
                init();
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
