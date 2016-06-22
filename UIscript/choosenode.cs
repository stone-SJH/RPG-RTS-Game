using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class choosenode : MonoBehaviour
{

    public EventSystem eventsystem;
    public GraphicRaycaster graphicRaycaster;
    public Canvas ca;
    public Text txt;
    private Text mytxt;

    public ParticleSystem ps;
    private ParticleSystem myps;


    private GameModeSwitch gms;
    public Camera RTScamera;

    //是否被选中
    private bool Ifcatch;

    private string name;

    private string parent_name;

    private GameObject pss;


    void Awake()
    {
        name = this.gameObject.name;
        //RimColor = new Color(0.2F, 0.8F, 10.6F, 1);

        Ifcatch = false;
        parent_name = this.transform.parent.gameObject.name;
        GameObject go1 = GameObject.Find("GameLogicManager");
        gms = go1.transform.GetComponent<GameModeSwitch>();
        GameObject go2 = GameObject.Find("RTSCamera");
        RTScamera = go2.transform.GetComponent<Camera>();
        pss = this.transform.FindChild("GroundHeal01").gameObject;
    }

    void Start()
    {
        
    }

    void Update()
    {
        //获取鼠标位置  
        Vector3 mPos = Input.mousePosition;

        if (CheckGuiRaycastObjects())
        {

            deleteEffect();


            return;
        }
        if (gms.RTSmode)
        {
            //向物体发射射线  
            Ray mRay = RTScamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit mHit;
            //射线检验  
            if (Physics.Raycast(mRay, out mHit, Mathf.Infinity, 1 << 9))
            {
                //射线击中当前物体，表示鼠标指向该物体

                if (mHit.collider.gameObject.transform.parent != null && mHit.collider.gameObject.transform.parent.gameObject.name == parent_name)
                {
                    //更改shader方法
                    makeEffect();


                    //鼠标左键点击选中
                    if (Input.GetMouseButtonDown(0))
                    {
                        //MakeInfo();
                        if (this.transform.parent.GetComponent<PathNode>().state != 0)
                        {
                            StartCoroutine(choose());
                        }
                        


                    }
                }
                //射线未击中当前物体，表示鼠标未指向该物体
                else
                {

                    deleteEffect();


                    //鼠标左键点击取消
                    if (Input.GetMouseButtonDown(0))
                    {

                    }
                }
            }
            //表示射线未击中任何物体
            else
            {


                deleteEffect();


                //鼠标左键点击取消
                if (Input.GetMouseButtonDown(0))
                {

                }
            }
        }
        updateColor();
    }
    //点击事件

    void MakeInfo()
    {

        if (Ifcatch == false)
        {
            mytxt = (Text)Instantiate(txt);
            mytxt.GetComponent<Transform>().SetParent(ca.GetComponent<Transform>());
            mytxt.text = "crystal";
            mytxt.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        }

    }
    void DeleteInfo()
    {
        Destroy(mytxt.gameObject);
    }

    void makeEffect()
    {
        if (myps == null)
        {
            myps = (ParticleSystem)Instantiate(ps);
            myps.GetComponent<Transform>().SetParent(this.GetComponent<Transform>());
            myps.GetComponent<Transform>().localPosition = new Vector3(0, 0, 0);
        }
    }

    void deleteEffect()
    {
        if (myps != null)
        {
            Destroy(myps.gameObject);
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
    public bool getStatus()
    {
        return Ifcatch;
    }


    IEnumerator choose()
    {
        
        if (this.transform.parent.GetComponent<PathNode>().ID == 2)
        {
            this.transform.parent.transform.parent.GetComponent<RouteManager>().AddToRoute(this.transform.parent.GetComponent<PathNode>());
            this.transform.parent.transform.parent.GetComponent<RouteManager>().FinishRoute();
            ArrayList newst = new ArrayList(select.selecttroops);
            int curNo = this.transform.parent.transform.parent.GetComponent<RouteManager>().No - 1;
            foreach (GameObject obj in newst)
            {
                makeTroop.positions[new Vector2(obj.transform.position.x, obj.transform.position.z)] = false;
                
                obj.GetComponent<Move>().cur = this.transform.parent.transform.parent.GetComponent<RouteManager>().start[curNo];
                yield return new WaitForSeconds(2.0f);
                Debug.Log("start move");
                select.troops.Remove(obj);
                obj.transform.GetComponent<Troop>().se.active = false;
            }
            
            Debug.Log("finish node");
        }
        else
        {
            this.transform.parent.transform.parent.GetComponent<RouteManager>().AddToRoute(this.transform.parent.GetComponent<PathNode>());
            this.transform.parent.transform.parent.GetComponent<PathManager>().PathnodeAccessableCheck(this.transform.parent.GetComponent<PathNode>());
            Debug.Log("add node");
        }
    }

    void updateColor()
    {
        if (this.transform.parent.GetComponent<PathNode>().state == 0) {
            pss.GetComponent<ParticleSystem>().startColor = Color.red;
        }
        else if (this.transform.parent.GetComponent<PathNode>().state == 2)
        {
            pss.GetComponent<ParticleSystem>().startColor = Color.green;
        }
        else if (this.transform.parent.GetComponent<PathNode>().state == 1)
        {
            pss.GetComponent<ParticleSystem>().startColor = Color.yellow;
        }
    }


}
