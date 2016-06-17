using UnityEngine;
using System.Collections;

public class GameLevelLogic : MonoBehaviour {
	public int level;
	public float levelTime;
	public int initialCrystals;

	public TargetBuilding tb;
	public Hero hero;
	public float gameTime;

	public bool winFlag;
	public bool loseFlag;

	public int rewardExp;
	public int rewardGold;
	public int[] rewardItems;

	// Use this for initialization
	void Start () {
		hero = GameObject.Find ("Hero").GetComponent<Hero> ();
		winFlag = false;
		loseFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
		gameTime += Time.deltaTime;
		if (tb.HP <= 0) {
			hero.exp += rewardExp;
			hero.golds += rewardGold;
			hero.golds += hero.crystals;
			foreach(int i in rewardItems){
				hero.AddItem(i, 1);
			}
			hero.HeroLevelUpCheck();
			hero.SyncItem();
			winFlag = true;
		}
		if (gameTime >= levelTime)
			loseFlag = true;
	
	}
}
