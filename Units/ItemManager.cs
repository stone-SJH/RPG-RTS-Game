using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

/* 背包系统 */
public class ItemCache{
	public int ID;
	public string Attributes;
}	

public class Item{
	public int itemID;
	public int itemNumber;
	public bool canUse;//是否为可以使用的

}


public class ItemManager : MonoBehaviour {

	// Use this for initialization
	public ArrayList Cache = new ArrayList();
	public bool CacheDone = false; 

	private string ItemTypeFileName = "cfg/Items.cfg";

	public void BuildCache(){
		StreamReader sr = new StreamReader (ItemTypeFileName, Encoding.GetEncoding("gb2312"));
		string sLine = "";
		while ((sLine = sr.ReadLine()) != null) {
			sLine.Trim ();
			if (sLine [0] == '#')
				continue;
			string[] splitArray = sLine.Split (new char[1]{';'}, 2, StringSplitOptions.RemoveEmptyEntries);
			ItemCache ic = new ItemCache ();
			ic.ID = int.Parse (splitArray [0]);
			ic.Attributes = splitArray [1];
			Cache.Add (ic);
		}
		CacheDone = true;
	}

	public ItemCache FindInCache(int ID){
		foreach (ItemCache ic in Cache) {
			if (ic.ID == ID)
				return ic;
		}
		return null;
	}

	void Start () {
		if (!CacheDone)
			BuildCache ();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
