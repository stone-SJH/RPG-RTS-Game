using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class skill6Use : MonoBehaviour {

    public Hero hero;
    private int level;
    private bool activited;
    
    private GameObject mycdimg;
    private float imglengthx;
    private float imglengthy;

    // Use this for initialization
    void Start () {
        mycdimg = this.transform.FindChild("cdImg").gameObject;
        imglengthx = mycdimg.transform.GetComponent<RectTransform>().sizeDelta.x;
        imglengthy = mycdimg.transform.GetComponent<RectTransform>().sizeDelta.y;
        mycdimg.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(imglengthx, 0);
    }
	
	// Update is called once per frame
	void Update () {
        level = hero.skill6.level;
        activited = hero.skill6.activited;
        if (level > 0)
        {
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("cure");
        }
        float skillstate = hero.Skill6State();
        if (skillstate != -1f && skillstate != -2f)
        {
            if (mycdimg != null)
            {
                mycdimg.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(imglengthx,
                    imglengthy*skillstate / hero.skill6.coolDown);
                mycdimg.transform.GetComponent<RectTransform>().localPosition = new Vector3(mycdimg.transform.GetComponent<RectTransform>().localPosition.x,
                    -0.5f*  imglengthy * (hero.skill6.coolDown- skillstate) / hero.skill6.coolDown, mycdimg.transform.GetComponent<RectTransform>().localPosition.z);
            }
        }
    }

    public void useSkill6()
    {
        float skillstate = hero.Skill6State();
        if (skillstate == -1f)
        {
            hero.UseSkill6();

            mycdimg.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(imglengthx, imglengthy);
            mycdimg.transform.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);


        }
    }

}
