using UnityEngine;
using System.Collections;

public class GameLevelLogic : MonoBehaviour {
	public int level;
	public float levelTime;
	public int initialCrystals;
	public GameObject victory;
	public GameObject lose;
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
		hero.crystals = initialCrystals;
	}
	
	// Update is called once per frame
	void Update () {
		gameTime += Time.deltaTime;
		if (!winFlag && tb.HP <= 0) {
			//胜利
			hero.exp += rewardExp;
			hero.golds += rewardGold;
			hero.golds += hero.crystals;
			foreach(int i in rewardItems){
				hero.AddItem(i, 1);
			}
			hero.HeroLevelUpCheck();
			hero.SyncItem();
			winFlag = true;
			GameObject myVictory = Instantiate(victory);
			myVictory.transform.parent = GameObject.Find("Canvas").transform;
			myVictory.transform.localPosition = new Vector3(0, 0, 0);
			//Time.timeScale = 0;
		}
		if (gameTime >= levelTime) {
			//失败
			if(loseFlag==false){
				GameObject mylose=Instantiate(lose);
				mylose.transform.parent=GameObject.Find("Canvas").transform;
				mylose.transform.localPosition=new Vector3(0,0,0);
				//Time.timeScale = 0;
			}
			loseFlag = true;

		}
	
	}
}
