using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class changeMode : MonoBehaviour {

	public GameObject gameMenu;
	private GameObject mygm;

	public bool pause;

	// Use this for initialization
	void Start () {
		pause = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) && pause == false)
		{
			mygm = Instantiate(gameMenu);
			mygm.transform.parent = GameObject.Find("Canvas").transform;
			mygm.transform.localPosition = new Vector3(0, 0, 0);
			mygm.GetComponent<gameMenu>().hc = this.gameObject;
			Time.timeScale = 0;
			pause = true;
		}
	}


    public void change()
    {
        GameModeSwitch gms = GameObject.Find("GameLogicManager").GetComponent<GameModeSwitch>();
        if (gms.RTSmode && GameObject.Find("Hero").GetComponent<Hero>().HP>0)
        {
           
            gms.SwitchToRPG();
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("rtsButton");
        }
        else if (gms.RPGmode)
        {
            gms.SwitchToRTS();
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("rpgButton");
        }
    }

}
