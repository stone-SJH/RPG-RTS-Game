using UnityEngine;
using System.Collections;

public class Treasure : MonoBehaviour {
	public Hero hero;
	public int crystals;
	public int[] items;

	public float radius = 5f;
	public float openTime = 10f;

	public bool inOpenState = false;
	public float inOpenTime = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		/*for test
		if (!inOpenState)
			StartOpen ();
		*/
		if ((Vector3.Distance (hero.transform.position, this.transform.position) <= radius) && hero.HP > 0)
			inOpenTime += Time.deltaTime;
		else {
			inOpenTime = 0f;
			inOpenState = false;
			hero.GetComponent<Controller> ().canMove = true;
		}
		if (inOpenTime >= openTime) {
			foreach (int i in items)
				hero.AddItem(i, 1);
			hero.crystals += crystals;
			hero.GetComponent<Controller>().canMove = true;
			Destroy(gameObject);
		}
	}

	public bool StartOpen(){
		//return true 表示开始打开箱子， false 表示因距离太远而失败
		Debug.Log(Vector3.Distance (hero.transform.position, this.transform.position));
		if (Vector3.Distance (hero.transform.position, this.transform.position) <= radius) {
			inOpenState = true;
			hero.GetComponent<Controller>().canMove = false;
			return true;
		}
		return false;
	}
}
