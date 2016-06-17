using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class skill9Use : MonoBehaviour {

    public Hero hero;
    private int level;
    private bool activited;

    private GameObject mycdimg;
    private float imglengthx;
    private float imglengthy;

    private bool inChoosePosition;

    public EventSystem eventsystem;
    public GraphicRaycaster graphicRaycaster;
    public Canvas ca;

	public Camera aimCamera;

    // Use this for initialization
    void Start () {
        mycdimg = this.transform.FindChild("cdImg").gameObject;
        imglengthx = mycdimg.transform.GetComponent<RectTransform>().sizeDelta.x;
        imglengthy = mycdimg.transform.GetComponent<RectTransform>().sizeDelta.y;
        mycdimg.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(imglengthx, 0);
        inChoosePosition = false;
    }
	
	// Update is called once per frame
	void Update () {
        level = hero.skill9.level;
        activited = hero.skill9.activited;
        if (level > 0)
        {
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("blizzard");
        }
        float skillstate = hero.Skill9State();
        if (skillstate != -1f && skillstate != -2f)
        {
            if (mycdimg != null)
            {
                mycdimg.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(imglengthx,
                    imglengthy * skillstate / hero.skill9.coolDown);
                mycdimg.transform.GetComponent<RectTransform>().localPosition = new Vector3(mycdimg.transform.GetComponent<RectTransform>().localPosition.x,
                    -0.5f * imglengthy * (hero.skill9.coolDown - skillstate) / hero.skill9.coolDown, mycdimg.transform.GetComponent<RectTransform>().localPosition.z);
            }
        }
       
        if (inChoosePosition)
        {
            Vector3 mPos = Input.mousePosition;

            if (Input.GetMouseButtonDown(1))
            {
				inChoosePosition = false;
				hero.Skill9CancelAimState();
                this.GetComponent<Image>().color = Color.white;
            }

            if (CheckGuiRaycastObjects())
            {
                
                return;
            }

            //向物体发射射线  
            Ray mRay = aimCamera.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit mHit;

            

            //射线检验  
            if (Physics.Raycast(mRay, out mHit, Mathf.Infinity))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    hero.UseSkill9(mHit.point);
					mycdimg.transform.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
					hero.Skill9CancelAimState();
                    this.GetComponent<Image>().color = Color.white;
                }
                if (Input.GetMouseButtonDown(1))
                {
					inChoosePosition = false;
					hero.Skill9CancelAimState();
                    this.GetComponent<Image>().color = Color.white;
                }

            }
        }
    }

    public void useSkill9()
    {
        float skillstate = hero.Skill9State();
        if (skillstate == -1f )
        {
            
            if (!inChoosePosition)
            {
                //hero.UseSkill9();
				hero.Skill9AimState();
                inChoosePosition = true;
                this.GetComponent<Image>().color = Color.green;
                //mycdimg.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(imglengthx, imglengthy);
            }
            else
            {
                inChoosePosition = false;
				hero.Skill9CancelAimState();
                this.GetComponent<Image>().color = Color.white;
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

}
