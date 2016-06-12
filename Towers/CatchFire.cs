using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CatchFire : MonoBehaviour
{

    public EventSystem eventsystem;
    public GraphicRaycaster graphicRaycaster;
    public Canvas ca;
    public Text txt;
    private Text mytxt;

    public ParticleSystem ps;
    private ParticleSystem myps;

    
    //是否被选中
    private bool Ifcatch;

    private string name;


    void Start()
    {
        name = this.gameObject.name;
        
        Ifcatch = false;
    }

    void Update()
    {
        //获取鼠标位置  
        Vector3 mPos = Input.mousePosition;

        if (CheckGuiRaycastObjects())
        {
            if (Ifcatch == false)
            {
                deleteEffect();
                
            }
            return;
        }

        //向物体发射射线  
        Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit mHit;
        //射线检验  
        if (Physics.Raycast(mRay, out mHit))
        {
            //射线击中当前物体，表示鼠标指向该物体

            if (mHit.collider.gameObject.name == name)
            {
                //更改shader方法
                makeEffect();
                

                //鼠标左键点击选中
                if (Input.GetMouseButtonDown(0))
                {
                    MakeInfo();
                    Click();

                }
            }
            //射线未击中当前物体，表示鼠标未指向该物体
            else
            {
                if (Ifcatch == false)
                {
                    deleteEffect();
                    
                }
                //鼠标左键点击取消
                if (Input.GetMouseButtonDown(0))
                {
                    if (Ifcatch == true)
                        DeleteInfo();
                    Cancel();
                }
            }
        }
        //表示射线未击中任何物体
        else
        {
            if (Ifcatch == false)
            {
                deleteEffect();
                
            }
            //鼠标左键点击取消
            if (Input.GetMouseButtonDown(0))
            {
                if (Ifcatch == true)
                    DeleteInfo();
                Cancel();
            }
        }

    }
    //点击事件
    void Click()
    {
        Ifcatch = true;

    }
    void Cancel()
    {
        Ifcatch = false;

    }
    void MakeInfo()
    {

        if (Ifcatch == false)
        {
            mytxt = (Text)Instantiate(txt);
            mytxt.GetComponent<Transform>().SetParent(ca.GetComponent<Transform>());
            mytxt.text = "Fire";
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
}
