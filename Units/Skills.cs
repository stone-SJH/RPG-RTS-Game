using UnityEngine;
using System.Collections;

//技能表
/*
 * 第一层 被动加生命（SKILL1）3/3
 * 第二层 被动加攻击力（SKILL2）3/3   被动加移速（SKILL3）3/3
 * 第三层 加生命光环（SKILL4）3/3     群体治疗（SKILL6）3/3
 * 		 加移速光环（SKILL5）3/3     保护罩（SKILL7）3/3  
 * 第四层 爆发（SKILL8）5/5          暴风雪（SKILL9）5/5
*/
public class Skill1{
	public bool activited = true;//是否能点
	public int level = 0;
	public int maxLevel = 3;
	public float ratio = 150f;

	public string introduction = "英雄的血统能使你更加强壮";

	public float AddHp(){
		return level * ratio;
	}

	public string GetAbility(){
		string ability = "被动效果：增加生命 (" + ((1 * ratio).ToString ()) + "/" + (2 * ratio).ToString() + "/" + (3 * ratio).ToString() + ")";
		return ability;
	}
}

public class Skill2{
	public bool activited = false;
	public int level = 0;
	public int maxLevel = 3;
	public float ratio = 25f;

	public string introduction = "武器上的法印强化了你的力量";
	
	public float AddAttack(){
		return level * ratio;
	}
	public string GetAbility(){
		string ability = "被动效果：增加攻击力 (" + ((1 * ratio).ToString ()) + "/" + (2 * ratio).ToString() + "/" + (3 * ratio).ToString() + ")";
		return ability;
	}
	
}

public class Skill3{
	public bool activited = false;
	public int level = 0;
	public int maxLevel = 3;
	public float ratio = 2f;

	public string introduction = "灌输了风之力的鞋子使你更加灵动";
	
	public float AddSpeed(){
		return level * ratio;
	}
	public string GetAbility(){
		string ability = "被动效果：增加移速 (" + ((1 * ratio).ToString ()) + "/" + (2 * ratio).ToString() + "/" + (3 * ratio).ToString() + ")";
		return ability;
	}
}

public class Skill4{
	public bool activited = false;
	public int level = 0;
	public int maxLevel = 3;
	public float initialRadius = 20f;
	public float radiusRatio = 5f;
	public float HPRatio = 60f;

	public string introduction = "你的存在鼓舞了身边军队的士气";
	
	public float getRadius(){
		if (level > 0)
			return initialRadius + level * radiusRatio;
		else
			return 0f;
	}
	public float getAddHP(){
		return level * HPRatio;
	}
	public string GetAbility(){
		string ability = "光环效果：增加周围一定范围 (" + ((initialRadius + 1 * radiusRatio).ToString ()) + "/" + (initialRadius + 2 * radiusRatio).ToString()+ "/" + (initialRadius + 3 * radiusRatio).ToString() + ")" + "内单位生命 (" + ((1 * HPRatio).ToString ()) + "/" + (2 * HPRatio).ToString() + "/" + (3 * HPRatio).ToString() + ")";
		return ability;
	}
}

public class Skill5{
	public bool activited = false;
	public int level = 0;
	public int maxLevel = 3;
	public float initialRadius = 20f;
	public float radiusRatio = 5f;
	public float speedRatio = 1.5f;

	public string introduction = "你的激励使得身边军队变得更加灵动";
	
	public float getRadius(){
		if (level > 0)
			return initialRadius + level * radiusRatio;
		else
			return 0f;
	}
	public float getAddSpeed(){
		return level * speedRatio;
	}
	public string GetAbility(){
		string ability = "光环效果：增加周围一定范围 (" + ((initialRadius + 1 * radiusRatio).ToString ()) + "/" + (initialRadius + 2 * radiusRatio).ToString()+ "/" + (initialRadius + 3 * radiusRatio).ToString() + ")" + "内单位移速 (" + ((1 * speedRatio).ToString ()) + "/" + (2 * speedRatio).ToString() + "/" + (3 * speedRatio).ToString() + ")";
		return ability;
	}
}

public class Skill6{
	public bool activited = false;
	
	public int level = 0;
	public int maxLevel = 3;
	public float initialRadius = 22f;
	public float radiusRatio = 8f;
	public float initialHeal = 200f;
	public float healRatio = 40f;
	
	public bool available = true;
	public float coolDown = 30f;
	public float inCDTime = 0f;

	public string introduction = "你低声念出一段咒语，恢复周围单位生命值";
	
	public float getRadius(){
		if (level > 0)
			return initialRadius + level * radiusRatio;
		else
			return 0f;
	}
	
	public float getHeal(){
		if (level > 0)
			return initialHeal + level * healRatio;
		else
			return 0f;
	}
	
	public string GetAbility(){
		string ability = "主动使用：恢复周围一定范围 (" + ((initialRadius + 1 * radiusRatio).ToString ()) + "/" + (initialRadius + 2 * radiusRatio).ToString() + "/" + (initialRadius + 3 * radiusRatio).ToString() + ")" + "内单位生命 (" + ((initialHeal + 1 * healRatio).ToString ()) + "/" + (initialHeal + 2 * healRatio).ToString() + "/" + (initialHeal + 3 * healRatio).ToString() + ")" + ", CD为" + coolDown.ToString() + "s";
		return ability;
	}
}

public class Skill7{
	public bool activited = false;
	
	public int level = 0;
	public int maxLevel = 3;
	public float initialRadius = 22f;
	public float radiusRatio = 8f;
	public float initialLastTime = 3f;
	public float lastTimeRatio = 1f;
	public float inLastTime = 0f;
	
	public bool available = true;
	public float coolDown = 20f;
	public float inCDTime = 0f;

	public string introduction = "信仰加持能使英雄及身边部队豁免所受的伤害";
	
	public float getRadius(){
		if (level > 0)
			return initialRadius + level * radiusRatio;
		else
			return 0f;
	}
	
	public float getLastTime(){
		if (level > 0)
			return initialLastTime + level * lastTimeRatio;
		else
			return 0f;
	}
	public string GetAbility(){
		string ability = " 主动效果：使英雄自身及周围一定范围 (" + ((initialRadius + 1 * radiusRatio).ToString ()) + "/" + (initialRadius + 2 * radiusRatio).ToString()+ "/" + (initialRadius + 3 * radiusRatio).ToString() + ")" + "内单位在一定时间 (" + ((initialLastTime + 1 * lastTimeRatio).ToString ()) + "/" + (initialLastTime + 2 * lastTimeRatio).ToString() + "/" + (initialLastTime + 3 * lastTimeRatio).ToString() + ")s" + "内免疫一切伤害"+ ", CD为" + coolDown.ToString() + "s";
		return ability;
	}

}

public class Skill8{
	public bool activited = false;
	
	public int level = 0;
	public int maxLevel = 5;
	public float initialLastTime = 5f;
	public float lastTimeRatio = 1f;
	public float inLastTime = 0f;
	
	public bool available = true;
	public float coolDown = 10f;
	public float inCDTime = 0f;

	public string introduction = "瞬间引导大量风元素灌注全身，使英雄的移速大幅提升";
	
	public float getLastTime(){
		if (level > 0)
			return initialLastTime + level * lastTimeRatio;
		else 
			return 0f;
	}
	public string GetAbility(){
		string ability = " 主动效果：在一段时间 (" + ((initialLastTime + 1 * lastTimeRatio).ToString ()) + "/" + (initialLastTime + 2 * lastTimeRatio).ToString() + "/" +(initialLastTime + 3 * lastTimeRatio).ToString() + "/" + (initialLastTime + 4 * lastTimeRatio).ToString() + "/" + (initialLastTime + 5 * lastTimeRatio).ToString() + ")s" + "内提升移速至极限 (60)"+ ", CD为" + coolDown.ToString() + "s"; 
		return ability;
	}
}

public class Skill9{ 
	public bool activited = false;
	
	public int level = 0;
	public int maxLevel = 5;
	public float initialRadius = 50f;
	public float radiusRatio = 10f;
	public float initialLastTime = 10f;
	public float lastTimeRatio = 1f;
	public float slowRatio = 0.4f;
	public float inLastTime = 0f;
	
	public bool available = true;
	public float coolDown = 30f;
	public float inCDTime = 0f;

	public string introduction = "召唤一阵暴风雪，在一段时间内减慢甚至冻结敌人的防御塔";
	
	public float getRadius(){
		if (level > 0)
			return initialRadius + level * radiusRatio;
		else
			return 0f;
	}
	
	public float getLastTime(){
		if (level > 0)
			return initialLastTime + level * lastTimeRatio;
		else
			return 0f;
	}
	public float getSlowRatio(){
		return slowRatio;
	}
	public string GetAbility(){
 		string ability = "光环效果：减慢指定范围 (" + ((initialRadius + 1 * radiusRatio).ToString ()) + "/" + (initialRadius + 2 * radiusRatio).ToString()+ "/" + (initialRadius + 3 * radiusRatio).ToString() + "/" + (initialRadius + 4 * radiusRatio).ToString() + "/" + (initialRadius + 5 * radiusRatio).ToString() + ")" + "区域内防御塔的攻击速度" + (slowRatio * 100).ToString() + "%，持续时间 (" + ((initialLastTime + 1 * lastTimeRatio).ToString ()) + "/" + (initialLastTime + 2 * lastTimeRatio).ToString() + "/" + (initialLastTime + 3 * lastTimeRatio).ToString() + "/" + (initialLastTime + 4 * lastTimeRatio).ToString() + "/" + (initialLastTime + 5 * lastTimeRatio).ToString()+ ")s"+ ", CD为" + coolDown.ToString() + "s";
		return ability;
	}
}

public class Skills : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
