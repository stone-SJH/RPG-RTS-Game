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
	
	public float AddHp(){
		return level * ratio;
	}
}

public class Skill2{
	public bool activited = false;
	public int level = 0;
	public int maxLevel = 3;
	public float ratio = 25f;
	
	public float AddAttack(){
		return level * ratio;
	}
}

public class Skill3{
	public bool activited = false;
	public int level = 0;
	public int maxLevel = 3;
	public float ratio = 2f;
	
	public float AddSpeed(){
		return level * ratio;
	}
}

public class Skill4{
	public bool activited = false;
	public int level = 0;
	public int maxLevel = 3;
	public float initialRadius = 20f;
	public float radiusRatio = 5f;
	public float HPRatio = 60f;
	
	public float getRadius(){
		if (level > 0)
			return initialRadius + level * radiusRatio;
		else
			return 0f;
	}
	public float getAddHP(){
		return level * HPRatio;
	}
}

public class Skill5{
	public bool activited = false;
	public int level = 0;
	public int maxLevel = 3;
	public float initialRadius = 20f;
	public float radiusRatio = 5f;
	public float speedRatio = 1.5f;
	
	public float getRadius(){
		if (level > 0)
			return initialRadius + level * radiusRatio;
		else
			return 0f;
	}
	public float getAddSpeed(){
		return level * speedRatio;
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
	
	public float getLastTime(){
		if (level > 0)
			return initialLastTime + level * lastTimeRatio;
		else 
			return 0f;
	}
}

public class Skill9{ 
	public bool activited = false;
	
	public int level = 0;
	public int maxLevel = 5;
	public float initialRadius = 7.8f;
	public float radiusRatio = 2.2f;
	public float initialLastTime = 15f;
	public float lastTimeRatio = 5f;
	public float slowRatio = 0.5f;
	public float inLastTime = 0f;
	
	public bool available = true;
	public float coolDown = 30f;
	public float inCDTime = 0f;
	
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
}

public class Skills : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
