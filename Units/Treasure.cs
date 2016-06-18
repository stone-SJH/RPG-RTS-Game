using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Treasure : MonoBehaviour {
	public Hero hero;
	public int crystals;
	public int[] items;

	public float radius = 5f;
	public float openTime = 10f;

	public bool inOpenState = false;
	public float inOpenTime = 0f;

    public GameObject notice;
    private GameObject mynotice;

    public GameObject schedule;
    private GameObject mysc;

    public GameObject selecteffect;
    private GameObject myse;
    public Camera cam;

    public EventSystem eventsystem;
    public GraphicRaycaster graphicRaycaster;
    public Canvas ca;

    private GameModeSwitch gms;

    private string name;

    // Use this for initialization
    void Start () {
        name = this.gameObject.name;
        gms = GameObject.Find("GameLogicManager").transform.GetComponent<GameModeSwitch>();
        cam = GameObject.Find("RPGCamera").transform.GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        /*for test
		if (!inOpenState)
			StartOpen ();
		*/
        

        if (inOpenState && (Vector3.Distance (hero.transform.position, this.transform.position) <= radius) && hero.HP > 0)
			inOpenTime += Time.deltaTime;
		
		if (inOpenTime >= openTime) {
			foreach (int i in items)
				hero.AddItem(i, 1);
			hero.crystals += crystals;
			hero.GetComponent<Controller>().canMove = true;
			Destroy(gameObject);
		}
        raycheck();
    }

	public bool canOpen(){
		//return true 表示开始打开箱子， false 表示因距离太远而失败
		if (Vector3.Distance (hero.transform.position, this.transform.position) <= radius) {
			//inOpenState = true;
			//hero.GetComponent<Controller>().canMove = false;
			return true;
		}
		return false;
	}

    public void startOpen()
    {

        mysc = Instantiate(schedule);
        mysc.transform.SetParent(this.transform);
        mysc.transform.localPosition = new Vector3(0, 0, 0);
        
        mysc.transform.localScale= new Vector3(1, 1, 1); 

        hero.GetComponent<Controller>().canMove = false;
        inOpenState = true;
    }

    void raycheck()
    {
        Vector3 mPos = Input.mousePosition;

        if (CheckGuiRaycastObjects())
        {
            
            return;
        }
        if (gms.RPGmode)
        {
            //向物体发射射线  
            Ray mRay = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit mHit;
            //射线检验  
            if (Physics.Raycast(mRay, out mHit, Mathf.Infinity))
            {
                //射线击中当前物体，表示鼠标指向该物体

                if (mHit.collider.gameObject.name == name)
                {
                    //更改shader方法
                    makeEffect();


                    //鼠标左键点击选中
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (canOpen())
                        {
                            mynotice = Instantiate(notice);
                            mynotice.transform.SetParent(ca.transform);
                            mynotice.transform.localPosition = new Vector3(0, 0, 0);
                            mynotice.transform.GetComponent<noticeWindow>().treasure = this.GetComponent<Treasure>();
                        }

                    }
                }
                //射线未击中当前物体，表示鼠标未指向该物体
                else
                {
                    
                        deleteEffect();

                    
                    
                    
                }
            }
            //表示射线未击中任何物体
            else
            {
                
            }
        }
    }

    bool CheckGuiRaycastObjects()
    {
        PointerEventData eventData = new PointerEventData(eventsystem);
        eventData.pressPosition = Input.mousePosition;
        eventData.position = Input.mousePosition;

        List<RaycastResult> list = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, list);
        //Debug.Log(list.Count);
        return list.Count > 0;
    }

    void makeEffect()
    {
        if (myse == null)
        {
            myse = Instantiate(selecteffect);
            myse.GetComponent<Transform>().SetParent(this.GetComponent<Transform>());
            myse.GetComponent<Transform>().localPosition = new Vector3(0, 0, 0);
        }
    }

    void deleteEffect()
    {
        if (myse != null)
        {
            Destroy(myse.gameObject);
        }
    }

}
