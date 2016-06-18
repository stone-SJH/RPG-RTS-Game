using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class ItemState{
	public int ID;

	public float extraHP;
	public float extraSpeed;
	public float extraAttack;
	public bool OP;

	public bool inCD;
	public float CD;
	public float inCDTime;

	public bool inBUFF;
	public float LastTime;
	public float inLastTime;
}

public class Hero : MonoBehaviour {
	//资源
	public int crystals = 1000;
	public int golds = 0;

	//public GameObject hero;
	public ItemManager im;
	public GameModeSwitch gms;
	public Item[] column = new Item[6];
	public ArrayList stash = new ArrayList (); 
	public GameObject aimCamera;

	public ArrayList itemStates = new ArrayList ();

	public string saveFileNamePrefix = "save/";
	public string saveFileNameSuffix = ".txt";
	public string heroName = "Stone";

	//basic information
	public int level = 1;
	public int skillPoint = 0;
	public float exp = 0;
	public int expHap = 1000;
	//速度极限值
	public float MaxSpeed = 60f;
	//升级加成
	public float HPGap = 50f;
	public float speedGap = 0.1f;
	public float attackGap = 1f;
	//当前值
	public float HP;
	public float speed;
	public float attack;
	//特殊额外值
	public float eltraHP;
	public float eltraSpeed;
	public float eltraAttack;
	//正常属性值
	public float maxHP = 1000f;
	public float ordSpeed = 25f;
	public float ordAttack = 100f;

	public Skill1 skill1 = new Skill1();
	public Skill2 skill2 = new Skill2();
	public Skill3 skill3 = new Skill3();
	public Skill4 skill4 = new Skill4();
	public Skill5 skill5 = new Skill5();
	public Skill6 skill6 = new Skill6();
	public Skill7 skill7 = new Skill7();
	public Skill8 skill8 = new Skill8();
	public Skill9 skill9 = new Skill9();

	//use in troop class
	public float addHP = 0f;
	public float addHPRadius = 0f;

	public float addSpeed = 0f;
	public float addSpeedRadius = 0f;

	public int healCount = 0;
	public float heal = 0f;
	public float healRadius = 0f;

	public bool isOPState = false;
	public float OPRadius = 0f;

	//use in tower class
	public bool isSlowedState = false;
	public float slowedRadius = 0f;
	public Vector3 slowedCenter = new Vector3 (0, 0, 0);
	public float slowedRatio = 0f;

	//use in this class
	public bool isBursted = false;
	public bool isOP = false;

	public bool isSlowState = false;
	public float slowRatio = 1f;
	public float slowLastTime = 0f;
	public float inSlowLastTime = 0f;

	void Access(){
		/*
		 *从文件中读入等级技能数据 
		 */
		string fileName = saveFileNamePrefix + "_Player_" + heroName + saveFileNameSuffix;
		StreamReader sr = new StreamReader (fileName);
		string sLine = "";
		int lineNo = 1;
		while ((sLine = sr.ReadLine()) != null) {
			sLine.Trim();
			if (sLine[0] == '#')
				continue;
			switch (lineNo){
			case 1:
			{
				string[] splitArray = sLine.Split(new char[2]{'_', ';'}, StringSplitOptions.RemoveEmptyEntries);
				level = int.Parse(splitArray[0]);
				eltraHP = float.Parse(splitArray[1]);
				eltraSpeed = float.Parse(splitArray[2]);
				eltraAttack = float.Parse(splitArray[3]);
				skillPoint = int.Parse(splitArray[4]);
				exp = float.Parse(splitArray[5]);
				golds = int.Parse(splitArray[6]);
				break;	
			}
			case 2:
			{
				string[] splitArray = sLine.Split(new char[2]{'_', ';'}, StringSplitOptions.RemoveEmptyEntries);
				skill1.level = int.Parse(splitArray[0]);
				skill2.level = int.Parse(splitArray[1]);
				skill3.level = int.Parse(splitArray[2]);
				skill4.level = int.Parse(splitArray[3]);
				skill5.level = int.Parse(splitArray[4]);
				skill6.level = int.Parse(splitArray[5]);
				skill7.level = int.Parse(splitArray[6]);
				skill8.level = int.Parse(splitArray[7]);
				skill9.level = int.Parse(splitArray[8]);
				break;	
			}
			default:
				break;
			}
			lineNo++;
		}
		sr.Close ();
		/*
		 *从文件中读入背包物品信息 
		 */
		if (!im.CacheDone)
			im.BuildCache ();
		fileName = saveFileNamePrefix + "_Stash_" + heroName + saveFileNameSuffix;
		sr = new StreamReader (fileName);
		sLine = "";
		int columnNo = 0;
		while ((sLine = sr.ReadLine()) != null) {
			sLine.Trim ();
			if (sLine [0] == '#')
				continue;
			string[] splitArray = sLine.Split (new char[2]{'_', ';'}, StringSplitOptions.RemoveEmptyEntries);
			Item item = new Item();
			item.itemID = int.Parse(splitArray[0]);
			item.itemNumber = int.Parse(splitArray[1]);
			ItemCache ic = new ItemCache();
			ic = im.FindInCache(item.itemID);
			string attributes = ic.Attributes;
			string[] splitArray2 = attributes.Split (new char[2]{'_', ';'}, StringSplitOptions.RemoveEmptyEntries);
			if (int.Parse(splitArray2[1]) == 0)
				item.canUse = false;
			else
				item.canUse = true;
			
			if (int.Parse(splitArray[2]) == 1){
				if (columnNo <= 5){
					column[columnNo] = item;
					columnNo++;
				}
				else
					stash.Add(item);
			}
			else
				stash.Add(item);
		}
		sr.Close ();
	}

	void Init(){
		//初始化信息
		maxHP = maxHP + level * HPGap + eltraHP;
		ordSpeed = ordSpeed + level * speedGap + eltraSpeed;
		ordAttack = ordAttack + level * attackGap + eltraAttack;

		maxHP += skill1.AddHp ();
		ordSpeed += skill3.AddSpeed ();
		ordAttack += skill2.AddAttack ();

		addHP = skill4.getAddHP ();
		addHPRadius = skill4.getRadius ();

		addSpeed = skill5.getAddSpeed ();
		addSpeedRadius = skill5.getRadius ();

		maxHP += addHP;
		ordSpeed += addSpeed;
	}

	void SyncHero(){
		//写文件
		//在英雄信息更改时调用
		//Debug.Log("sync");
		string fileName = saveFileNamePrefix + "_Player_" + heroName + saveFileNameSuffix;
		FileStream fs = new FileStream (fileName, FileMode.Truncate, FileAccess.Write);
		StreamWriter sw = new StreamWriter (fs);
		string inf = "";
		inf += "_" + level.ToString () + ";";
		inf += "_" + eltraHP.ToString () + ";";
		inf += "_" + eltraSpeed.ToString () + ";";
		inf += "_" + eltraAttack.ToString () + ";";
		inf += "_" + skillPoint.ToString () + ";";
		inf += "_" + exp.ToString () + ";";
		inf += "_" + golds.ToString () + ";";
		inf += "\n";
		inf += "_" + skill1.level.ToString () + ";";
		inf += "_" + skill2.level.ToString () + ";";
		inf += "_" + skill3.level.ToString () + ";";
		inf += "_" + skill4.level.ToString () + ";";
		inf += "_" + skill5.level.ToString () + ";";
		inf += "_" + skill6.level.ToString () + ";";
		inf += "_" + skill7.level.ToString () + ";";
		inf += "_" + skill8.level.ToString () + ";";
		inf += "_" + skill9.level.ToString () + ";";
		sw.Write (inf);
		sw.Flush ();
		sw.Close ();
		fs.Close ();
	}

	public void SyncItem(){
		string fileName = saveFileNamePrefix + "_Stash_" + heroName + saveFileNameSuffix;
		FileStream fs = new FileStream (fileName, FileMode.Truncate, FileAccess.Write);
		StreamWriter sw = new StreamWriter (fs);
		for (int i = 0; i < 6; i++) {
			if (column [i] != null) {
				string inf = "";
				inf += "_" + column [i].itemID + "_" + column [i].itemNumber + "_1\n";
				sw.Write(inf);
			}
		}
		foreach (Item item in stash) {
			string inf = "";
			inf += "_" + item.itemID + "_" + item.itemNumber + "_0\n";
			sw.Write(inf);
		}
		sw.Flush();
		sw.Close();
		fs.Close();
	}

	void TestChinese(){
		FileStream fs = new FileStream ("2.txt", FileMode.Create);
		StreamReader sr = new StreamReader ("1.txt", Encoding.GetEncoding("gb2312"));
		string sine = "";
		sine = sr.ReadLine ();
		Debug.Log (sine);
		StreamWriter sw = new StreamWriter (fs, Encoding.GetEncoding("gb2312"));
		sw.Write (sine);
		sw.Flush ();
		sr.Close ();
		sw.Close ();
		fs.Close ();
	}

	void EquipmentCheck(Item item){
		if (item.canUse)
			return;
		string attributes = im.FindInCache (item.itemID).Attributes;
		string[] splitArray1 = attributes.Split (new char[1]{';'}, StringSplitOptions.RemoveEmptyEntries);
		string[] splitArray2 = splitArray1[2].Split (new char[1]{'_'}, StringSplitOptions.RemoveEmptyEntries);
		int _level = int.Parse (splitArray2 [0]);
		int _HP = int.Parse (splitArray2[1]);
		int _speed = int.Parse(splitArray2[2]);
		int _attack = int.Parse (splitArray2 [3]);
		if (_HP != 0) {
			float _ratio = HP / maxHP;
			maxHP += _HP;
			HP = maxHP * _ratio;
		}
		if (_attack != 0) {
			attack += _attack;
			ordAttack += _attack;
		}
		if (_speed != 0)
			ordSpeed += _speed;
	}

	void EquipmentRemoveCheck(Item item){
		if (item.canUse)
			return;
		string attributes = im.FindInCache (item.itemID).Attributes;
		string[] splitArray1 = attributes.Split (new char[1]{';'}, StringSplitOptions.RemoveEmptyEntries);
		string[] splitArray2 = splitArray1[2].Split (new char[1]{'_'}, StringSplitOptions.RemoveEmptyEntries);
		int _level = int.Parse (splitArray2 [0]);
		int _HP = int.Parse (splitArray2[1]);
		int _speed = int.Parse(splitArray2[2]);
		int _attack = int.Parse (splitArray2 [3]);
		if (_HP != 0) {
			float _ratio = HP / maxHP;
			maxHP -= _HP;
			HP = maxHP * _ratio;
		}
		if (_attack != 0) {
			attack -= _attack;
			ordAttack -= _attack;
		}
		if (_speed != 0)
			ordSpeed -= _speed;
	}

	void ColumnEquipmentCheck(){
		for (int i = 0; i < 6; i++) {
			if (column [i] != null)
				EquipmentCheck (column [i]);
		}
	}

	// Use this for initialization
	void Start () {
		Access ();
		Init ();

		aimCamera.active = false;
		HP = maxHP;
		speed = ordSpeed;
		attack = ordAttack;
		ColumnEquipmentCheck ();
	}

	public void HeroLevelUpCheck(){
		while (exp >= expHap) {
			exp -= expHap;
			level++;
			skillPoint++;
			maxHP += HPGap;
			ordAttack += attackGap;
			ordSpeed += speedGap;
			HP = maxHP;
			attack = ordAttack;
			speed = ordSpeed;
		}
		SyncHero();
	}

	void SkillActivateCheck(){
		if (skill8.activited && skill9.activited)
			return;
		if (skill1.level > 0) {
			skill2.activited = true;
			skill3.activited = true;
		}
		if (skill2.level > 0) {
			skill4.activited = true;
			skill5.activited = true;
		}
		if (skill3.level > 0) {
			skill6.activited = true;
			skill7.activited = true;
		}
		if (skill4.level > 0 && skill5.level > 0)
			skill8.activited = true;
		if (skill6.level > 0 && skill7.level > 0)
			skill9.activited = true;
	}

	void SkillCDCheck(){
		if (!skill6.available)
			skill6.inCDTime += Time.deltaTime;
		if (skill6.inCDTime >= skill6.coolDown)
			skill6.available = true;

		if (!skill7.available)
			skill7.inCDTime += Time.deltaTime;
		if (skill7.inCDTime >= skill7.coolDown)
			skill7.available = true;
		if (isOPState)
			skill7.inLastTime += Time.deltaTime;
		if (skill7.inLastTime >= skill7.getLastTime ())
			isOPState = false;

		if (!skill8.available)
			skill8.inCDTime += Time.deltaTime;
		if (skill8.inCDTime >= skill8.coolDown)
			skill8.available = true;
		if (isBursted)
			skill8.inLastTime += Time.deltaTime;
		if (skill8.inLastTime >= skill8.getLastTime ())
			isBursted = false;

		if (!skill9.available)
			skill9.inCDTime += Time.deltaTime;
		if (skill9.inCDTime >= skill9.coolDown)
			skill9.available = true;
		if (isSlowedState)
			skill9.inLastTime += Time.deltaTime;
		if (skill9.inLastTime >= skill9.getLastTime ())
			isSlowedState = false;

	}

	void SlowStateCheck(){
		if (isSlowState) {
			inSlowLastTime += Time.deltaTime;
		}
		if (inSlowLastTime >= slowLastTime) {
			isSlowState = false;
			inSlowLastTime = 0f;
			slowLastTime = 0f;
		}
	}

	void SpeedDetermine(){
		speed = ordSpeed;
		if (this.transform.position.y < 5f)
			speed *= 0.5f;
		if (isSlowState)
			speed *= slowRatio;
		if (isBursted)
			speed = MaxSpeed;
	}

	void ItemStatesCheck(){
		ArrayList cleaner = new ArrayList ();
		foreach (ItemState ist in itemStates) {
			if (ist.OP)
				isOP = true;
			if (ist.inBUFF)
				ist.inLastTime += Time.deltaTime;
			if (ist.inCD)
				ist.inCDTime += Time.deltaTime;
			if (ist.inBUFF && ist.inLastTime >= ist.LastTime){
				float ratio = HP / maxHP;
				maxHP -= ist.extraHP;
				HP = ratio * maxHP;
				ordSpeed -= ist.extraSpeed;
				ordAttack -= ist.extraAttack;
				ist.inBUFF = false;
				if (ist.OP){
					ist.OP = false;
					isOP = false;
				}
			}
			if (ist.inCD && ist.inCDTime >= ist.CD){
				ist.inCD = false;
			}
			if (!ist.inCD && !ist.inBUFF)
				cleaner.Add(ist);
		}
		foreach (ItemState ist in cleaner)
			itemStates.Remove (ist);
	}

	// Update is called once per frame
	void Update () {
		/* for test
		int i = FindItemInColumn (12001);
		if (i != -1)
			UseItem (column [i], i);
		*/

		SlowStateCheck ();
		HeroLevelUpCheck ();
		SkillActivateCheck ();
		SkillCDCheck ();
		SpeedDetermine ();
		ItemStatesCheck ();
	}

	public void Skill1LevelUp(){
		if (skillPoint > 0 && skill1.activited && skill1.level < skill1.maxLevel) {
			maxHP -= skill1.AddHp ();
			skillPoint--;
			skill1.level++;
			maxHP += skill1.AddHp ();
			SyncHero();
		}
	}
	public void Skill2LevelUp(){
		if (skillPoint > 0 && skill2.activited && skill2.level < skill2.maxLevel) {
			ordAttack -= skill2.AddAttack ();
			skillPoint--;
			skill2.level++;
			ordAttack += skill2.AddAttack ();
			SyncHero();
		}
	}
	public void Skill3LevelUp(){
		if (skillPoint > 0 && skill3.activited && skill3.level < skill3.maxLevel) {
			ordSpeed -= skill3.AddSpeed ();
			skillPoint--;
			skill3.level++;
			ordSpeed += skill3.AddSpeed ();
			SyncHero();
		}
	}
	public void Skill4LevelUp(){
		if (skillPoint > 0 && skill4.activited && skill4.level < skill4.maxLevel) {
			skillPoint--;
			skill4.level++;
			addHP = skill4.getAddHP ();
			addHPRadius = skill4.getRadius ();
			SyncHero();
		}
	}
	public void Skill5LevelUp(){
		if (skillPoint > 0 && skill5.activited && skill5.level < skill5.maxLevel) {
			skillPoint--;
			skill5.level++;
			addSpeed = skill5.getAddSpeed ();
			addSpeedRadius = skill5.getRadius ();
			SyncHero();
		}
	}
	public void Skill6LevelUp(){
		if (skillPoint > 0 && skill6.activited && skill6.level < skill6.maxLevel) {
			skillPoint--;
			skill6.level++;
			SyncHero();
		}
	}
	public void Skill7LevelUp(){
		if (skillPoint > 0 && skill7.activited && skill7.level < skill7.maxLevel) {
			skillPoint--;
			skill7.level++;
			SyncHero();
		}
	}
	public void Skill8LevelUp(){
		if (skillPoint > 0 && skill8.activited && skill8.level < skill8.maxLevel) {
			skillPoint--;
			skill8.level++;
			SyncHero();
		}
	}
	public void Skill9LevelUp(){
		if (skillPoint > 0 && skill9.activited && skill9.level < skill9.maxLevel) {
			skillPoint--;
			skill9.level++;
			SyncHero();
		}
	}

	public float Skill6State(){
		/*
		 * return -1 说明可以使用该技能
		 * return -2 说明没点这个技能
		 * return 一个大于0的float 表示该技能的剩余冷却时间
		 */
		if (skill6.level == 0)
			return -2f;
		if (skill6.available)
			return -1f;
		return skill6.coolDown - skill6.inCDTime;
	}
	public void UseSkill6(){
		skill6.available = false;
		skill6.inCDTime = 0f;
		healCount++;
		heal = skill6.getHeal ();
		healRadius = skill6.getRadius ();
	}

	public float Skill7State(){
		/*
		 * return -1 说明可以使用该技能
		 * return -2 说明没点这个技能
		 * return 一个大于0的float 表示该技能的剩余冷却时间
		 */
		if (skill7.level == 0)
			return -2f;
		if (skill7.available)
			return -1f;
		return skill7.coolDown - skill7.inCDTime;
	}
	public void UseSkill7(){
		skill7.available = false;
		skill7.inCDTime = 0f;
		isOPState = true;
		skill7.inLastTime = 0f;
		OPRadius = skill7.getRadius ();
	}

	public float Skill8State(){
		/*
		 * return -1 说明可以使用该技能
		 * return -2 说明没点这个技能
		 * return 一个大于0的float 表示该技能的剩余冷却时间
		 */
		if (skill8.level == 0)
			return -2f;
		if (skill8.available)
			return -1f;
		return skill8.coolDown - skill8.inCDTime;
	}
	public void UseSkill8(){
		skill8.available = false;
		skill8.inCDTime = 0f;
		skill8.inLastTime = 0f;
		isBursted = true;
	}

	public float Skill9State(){
		/*
		 * return -1 说明可以使用该技能
		 * return -2 说明没点这个技能
		 * return 一个大于0的float 表示该技能的剩余冷却时间
		 */
		if (skill9.level == 0)
			return -2f;
		if (skill9.available)
			return -1f;
		return skill9.coolDown - skill9.inCDTime;
	}
	//传入点击的位置作为技能释放中心点
	public void UseSkill9(Vector3 CenterPoint){
		skill9.available = false;
		skill9.inCDTime = 0f;
		skill9.inLastTime = 0f;
		isSlowedState = true;
		slowedRatio = skill9.getSlowRatio ();
		slowedRadius = skill9.getRadius ();
		slowedCenter = CenterPoint;
	}

	public void GetSlowed(float ratio, float lastTime){
		isSlowState = true;
		slowRatio = ratio;
		slowLastTime = lastTime;
		inSlowLastTime = 0f;
	}

	public void Skill9AimState(){
		gms.SwitchToNULL ();
		aimCamera.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + 200f, this.transform.position.z);
		aimCamera.active = true;
		Time.timeScale = 0f;
	}

	public void Skill9CancelAimState(){
		gms.SwitchToRPG ();
		aimCamera.active = false;
		Time.timeScale = 1f;
	}

	public bool AddToColumn(Item item, int ColumnNo){
		if (ColumnNo < 6 && column [ColumnNo] == null) {
			column [ColumnNo] = item;
			stash.Remove(item);
			EquipmentCheck(item);
			return true;
		} else
			return false;
	}

	public bool Transfer(Item item, int ColumnNo){
		if (ColumnNo < 6 && column [ColumnNo] != null) {
			stash.Add(column[ColumnNo]);
			EquipmentRemoveCheck(column[ColumnNo]);
			column [ColumnNo] = item;
			stash.Remove(item);
			EquipmentCheck(item);
			ItemState ist = FindInItemStates(item.itemID);
			if (ist != null){
				if (ist.inCD){
					if (!item.canUse){
						SyncItem();
						return true;
					}
					string attributes = im.FindInCache (item.itemID).Attributes;
					string[] splitArray = attributes.Split (new char[2]{'_', ';'}, StringSplitOptions.RemoveEmptyEntries);
					int type = int.Parse (splitArray [1]);
					ItemState ist2 = new ItemState();
					float _CD = 0f;
					ist2.ID = item.itemID;
					if (type == 1) {
						_CD = float.Parse(splitArray[7]);
					}
					else if (type == 2){
						int tag = int.Parse(splitArray[3]);
						switch (tag){
						case 0:
							_CD = float.Parse(splitArray[7]);
							break;
						case 1:
							_CD = float.Parse(splitArray[5]);
							break;
						default:
							break;
						}
					}
					ist2.CD = _CD;
					ist2.inCD = true;
					ist2.inCDTime = 0f;
					itemStates.Add(ist2);
				}
			}
			return true;
		} else
			return false;
	}

	public bool RemoveFromColumn(Item item, int ColumnNo){
		if (ColumnNo < 6 && column [ColumnNo] != null) {
			Item i = FindItemInStash(item.itemID);
			if (i != null)
				i.itemNumber += item.itemNumber;
			else
				stash.Add(column[ColumnNo]);
			EquipmentRemoveCheck(column[ColumnNo]);
			column [ColumnNo] = null;
			return true;
		} else
			return false;
	}

	public Item FindItemInStash(int ID){
		foreach (Item item in stash) {
			if (item.itemID == ID)
				return item;
		}
		return null;
	}

	public int FindItemInColumn(int ID){
		for (int i = 0; i < 6; i++) {
			if (column[i] != null && column [i].itemID == ID)
				return i;
		}
		return -1;
	}

	public void AddItem(int ID, int count){
		for (int i = 0; i < 6; i++) {
			if (column[i] != null && column [i].itemID == ID) {
				column [i].itemNumber += count;
				SyncItem ();
				return;
			}
		}
		Item item = FindItemInStash (ID);
		if (item != null) {
			item.itemNumber += count;
		}
		else{
			item = new Item ();
			item.itemID = ID;
			item.itemNumber = count;
			ItemCache ic = new ItemCache();
			ic = im.FindInCache(item.itemID);
			string attributes = ic.Attributes;
			string[] splitArray = attributes.Split (new char[2]{'_', ';'}, StringSplitOptions.RemoveEmptyEntries);
			if (int.Parse(splitArray[1]) == 0)
				item.canUse = false;
			else
				item.canUse = true;
			stash.Add (item);
		}
	}

	public void SellItem(Item item, int sellCount){
		//目前设定只能卖stash里面的物品
		if (item.itemNumber >= sellCount) {
			string attributes = im.FindInCache (item.itemID).Attributes;
			string[] splitArray1 = attributes.Split (new char[1]{';'}, StringSplitOptions.RemoveEmptyEntries);
			string[] splitArray2 = splitArray1 [2].Split (new char[1]{'_'}, StringSplitOptions.RemoveEmptyEntries);
			int _cost = int.Parse (splitArray2 [4]);
			item.itemNumber -= sellCount;
			if (item.itemNumber == 0)
				stash.Remove(item);
			golds += (int)(Mathf.Floor(_cost * 0.5f));
			SyncHero();
			SyncItem();
		}
	}

	public ItemState FindInItemStates(int ID){
		foreach (ItemState ist in itemStates) {
			if (ist.ID == ID)
				return ist;
		}
		return null;
	}

	public void UseItem(Item item, int ColumnNo){
		if (!item.canUse)
			return;
		string attributes = im.FindInCache (item.itemID).Attributes;
		string[] splitArray = attributes.Split (new char[2]{'_', ';'}, StringSplitOptions.RemoveEmptyEntries);
		int type = int.Parse (splitArray [1]);
		if (type == 1) {
			float _addHP = float.Parse(splitArray[3]);
			float _extraHP = float.Parse(splitArray[4]);
			float _extraSpeed = float.Parse(splitArray[5]);
			float _lastTime = float.Parse(splitArray[6]);
			float _CD = float.Parse(splitArray[7]);
			ItemState ist = FindInItemStates(item.itemID);
			if (ist != null){
				if (!ist.inCD){
					ist.inBUFF = true;
					ist.inCD = true;
					ist.inCDTime = 0f;
					ist.inLastTime = 0f;
					HP += _addHP;
					if (HP >= maxHP)
						HP = maxHP;
				}
				else
					return;
			}
			else{
				ist = new ItemState();
				ist.ID = item.itemID;
				ist.inBUFF = true;
				ist.inCD = true;
				ist.inCDTime = 0f;
				ist.inLastTime = 0f;
				ist.LastTime = _lastTime;
				ist.CD = _CD;
				ist.extraHP = _extraHP;
				ist.extraSpeed = _extraSpeed;
				float ratio = HP / maxHP;
				maxHP += ist.extraHP;
				HP = ratio * maxHP;
				ordSpeed += ist.extraSpeed;
				HP += _addHP;
				if (HP >= maxHP)
					HP = maxHP;
				itemStates.Add(ist);
			}
			item.itemNumber--;
			if (item.itemNumber == 0){
				column[ColumnNo] = null;
			}
		} 
		else if (type == 2) {
			int tag = int.Parse(splitArray[3]);
			switch (tag){
			case 0:{
				//群体治疗
				float _heal = float.Parse(splitArray[4]);
				float _healRadius = float.Parse(splitArray[5]);
				float _lastTime = float.Parse(splitArray[6]);
				float _CD = float.Parse(splitArray[7]);
				ItemState ist = FindInItemStates(item.itemID);
				if (ist != null){
					if (!ist.inCD){
						ist.inBUFF = true;
						ist.inCD = true;
						ist.inCDTime = 0f;
						ist.inLastTime = 0f;
						healCount++;
						heal = _heal;
						healRadius = _healRadius;
					}
					else
						return;
				}
				else{
					ist = new ItemState();
					ist.ID = item.itemID;
					ist.inBUFF = true;
					ist.inCD = true;
					ist.inCDTime = 0f;
					ist.inLastTime = 0f;
					ist.LastTime = _lastTime;
					ist.CD = _CD;
					healCount++;
					heal = _heal;
					healRadius = _healRadius;
					itemStates.Add(ist);
				}
				item.itemNumber--;
				if (item.itemNumber == 0){
					column[ColumnNo] = null;
				}
				Debug.Log("use");
				break;
			} 
			case 1:{
				//英雄无敌
				float _lastTime = float.Parse(splitArray[4]);
				float _CD = float.Parse(splitArray[5]);
				ItemState ist = FindInItemStates(item.itemID);
				if (ist != null){
					if (!ist.inCD){
						ist.inBUFF = true;
						ist.inCD = true;
						ist.inCDTime = 0f;
						ist.inLastTime = 0f;
					}
					else
						return;
				}
				else{
					ist = new ItemState();
					ist.ID = item.itemID;
					ist.inBUFF = true;
					ist.inCD = true;
					ist.inCDTime = 0f;
					ist.inLastTime = 0f;
					ist.LastTime = _lastTime;
					ist.CD = _CD;
					ist.OP = true;
					itemStates.Add(ist);
				}
				item.itemNumber--;
				if (item.itemNumber == 0){
					column[ColumnNo] = null;
				}
				break;
			}
			}
		}
	}
}
