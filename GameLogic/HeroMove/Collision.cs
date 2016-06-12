using UnityEngine;
using System.Collections;

public class Collision : MonoBehaviour {
    //碰撞音效
    //public AudioSource music;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	/*
    //主动碰撞的对象名称
    string castName = null;
    //接收碰撞的对象名称
    string receiveName = null;

    IEnumerator Func()
    {
        yield return new WaitForSeconds(1);
        GameObject manageObject = GameObject.Find("_Manager");
        GameManager gm = manageObject.GetComponent<GameManager>();
        AudioController ac = manageObject.GetComponent<AudioController>();
        Destroy(ac);
        gm.flag = -1;
        DontDestroyOnLoad(manageObject);
        Application.LoadLevel("LoseMenu");
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject hitObject = hit.collider.gameObject;

        if (!hitObject.name.Equals("_Terrain"))
        {
            castName = gameObject.name;
            receiveName = hitObject.name;
            if (hitObject.tag.Equals("dest"))
            {
                GameObject manageObject = GameObject.Find("_Manager");
                GameManager gm = manageObject.GetComponent<GameManager>();
                AudioController ac = manageObject.GetComponent<AudioController>();
                Destroy(ac);
                gm.flag = 1;
                DontDestroyOnLoad(manageObject);
                Application.LoadLevel("WinMenu");

            }
            else if (hitObject.tag.Equals("enemytank"))
            {
                if (!music.isPlaying)
                {
                    music.Play();
                    StartCoroutine(Func());
                }
            }
        }
    }

    void OnGUI()
    {
        //for test
        if (castName != null && receiveName != null)
        {
            GUI.color = Color.black;
            GUI.Label(new Rect(100, 100, 200, 30), "主动碰撞的对象名称" + castName);
            GUI.Label(new Rect(100, 200, 200, 30), "接收碰撞的对象名称" + receiveName);
        }
    }
    */
}
