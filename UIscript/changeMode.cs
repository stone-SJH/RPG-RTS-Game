using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class changeMode : MonoBehaviour {



	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
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
