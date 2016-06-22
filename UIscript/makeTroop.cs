using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class makeTroop : MonoBehaviour {
	public Hero hero;

    public GameObject troop1;
    private GameObject mytroop1;

    public GameObject troop2;
    private GameObject mytroop2;

    public GameObject troop3;
    private GameObject mytroop3;

    public GameObject troop4;
    private GameObject mytroop4;

    public Vector2 start;
    public Vector2 end;
    public int rows;
    public int cols;
    public float y;

	public GameLevelLogic gll;

	static public int level; 

    static public Dictionary<Vector2, bool> positions = new Dictionary<Vector2, bool>();

    // Use this for initialization
    void Start () {
		//level = gll.level;
	}
	void Awake(){
		float dx = (end.x - start.x) / (rows - 1);
		float dy = (end.y - start.y) / (cols - 1);
		for(int i = 0; i < rows; i++)
		{
			for(int j = 0; j < cols; j++)
			{
				Vector2 p = new Vector2(start.x + j * dx, start.y + i * dy);
				positions.Add(p, false);
			}
		}

	}
	// Update is called once per frame
	void Update () {
		if (level != gll.level) {
			positions.Clear();
			float dx = (end.x - start.x) / (rows - 1);
			float dy = (end.y - start.y) / (cols - 1);
			for(int i = 0; i < rows; i++)
			{
				for(int j = 0; j < cols; j++)
				{
					Vector2 p = new Vector2(start.x + j * dx, start.y + i * dy);
					positions.Add(p, false);
				}
			}
		}
		level = gll.level;
	}

    public void makeTroop1()
    {
		GameLevelLogic gll = GameObject.Find("LevelLogicManager").GetComponent<GameLevelLogic>();
		if (hero.crystals < troop1.GetComponent<Troop> ().crystalCost)
			return;
		foreach (KeyValuePair<Vector2, bool> po in positions)
        {
            if (po.Value == false)
            {
				mytroop1 = Instantiate(troop1);
				if (gll.level == 1 || gll.level == 2)
					mytroop1.transform.Rotate(0, -90, 0);
				if (gll.level == 3)
					mytroop1.transform.Rotate(0, -45, 0);
                mytroop1.transform.position = new Vector3(po.Key.x, y, po.Key.y);
                positions[po.Key] = true;
				hero.crystals -= troop1.GetComponent<Troop>().crystalCost;
                break;
            }
        }
    }

    public void makeTroop2()
    {
		GameLevelLogic gll = GameObject.Find("LevelLogicManager").GetComponent<GameLevelLogic>();
		if (hero.crystals < troop2.GetComponent<Troop> ().crystalCost)
			return;
        foreach(KeyValuePair<Vector2,bool> po in positions)
        {
            if (po.Value == false)
            {
				mytroop2 = Instantiate(troop2);
				if (gll.level == 1 || gll.level == 2)
					mytroop2.transform.Rotate(0, -90, 0);
				if (gll.level == 3)
					mytroop2.transform.Rotate(0, -45, 0);
                mytroop2.transform.position = new Vector3(po.Key.x, y, po.Key.y);
                positions[po.Key] = true;
				hero.crystals -= troop2.GetComponent<Troop>().crystalCost;
                break;
            }
        }
    }

    public void makeTroop3()
    {
		GameLevelLogic gll = GameObject.Find("LevelLogicManager").GetComponent<GameLevelLogic>();
		if (hero.crystals < troop3.GetComponent<Troop> ().crystalCost)
			return;
        foreach (KeyValuePair<Vector2, bool> po in positions)
        {
            if (po.Value == false)
            {
				mytroop3 = Instantiate(troop3);
				if (gll.level == 1 || gll.level == 2)
					mytroop3.transform.Rotate(0, -90, 0);
				if (gll.level == 3)
					mytroop3.transform.Rotate(0, -45, 0);
                mytroop3.transform.position = new Vector3(po.Key.x, y, po.Key.y);
                positions[po.Key] = true;
				hero.crystals -= troop3.GetComponent<Troop>().crystalCost;
                break;
            }
        }

    }

    public void makeTroop4()
    {
		GameLevelLogic gll = GameObject.Find("LevelLogicManager").GetComponent<GameLevelLogic>();
		if (hero.crystals < troop4.GetComponent<Troop> ().crystalCost)
			return;
        foreach (KeyValuePair<Vector2, bool> po in positions)
        {
            if (po.Value == false)
			{
				mytroop4 = Instantiate(troop4);
				if (gll.level == 1 || gll.level == 2)
					mytroop4.transform.Rotate(0, -90, 0);
				if (gll.level == 3)
					mytroop4.transform.Rotate(0, -45, 0);
                mytroop4.transform.position = new Vector3(po.Key.x, y, po.Key.y);
                positions[po.Key] = true;
				hero.crystals -= troop4.GetComponent<Troop>().crystalCost;
                break;
            }
        }
    }


}
