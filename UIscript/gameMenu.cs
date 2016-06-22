using UnityEngine;
using System.Collections;

public class gameMenu : MonoBehaviour {

    public GameObject hc;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void conGame()
    {
        Time.timeScale = 1;
		if (hc != null)
        	hc.GetComponent<changeMode>().pause = false;
        Destroy(this.gameObject);
    }

    public void retTobegin()
    {
		Time.timeScale = 1;
		if (hc != null)
			hc.GetComponent<changeMode>().pause = false;
        Application.LoadLevel("begin");
    }

    public void exit()
    {
        Application.Quit();
    }

    public void nextLevel()
    {
		Time.timeScale = 1;
        int level = GameObject.Find("LevelLogicManager").GetComponent<GameLevelLogic>().level;
        if (level != 4)
        {
            Application.LoadLevel("level" + (level + 1).ToString()+"_start");
        }
    }

	public void restart(){
		Time.timeScale = 1;
		int level = GameObject.Find("LevelLogicManager").GetComponent<GameLevelLogic>().level;
		Application.LoadLevel ("level" + level.ToString()+"_start");
	}

}
