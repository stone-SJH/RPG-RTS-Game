using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class skill1Button: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Hero hero;
    private int level;
    private bool activited;
    private GameObject myinfo;
    public GameObject info;

    private string information;
    private string ll;

	// Use this for initialization
	void Start () {
        
        information = hero.skill1.introduction;
        ll = hero.skill1.GetAbility();
        
	}
	
	// Update is called once per frame
	void Update () {
        level = hero.skill1.level;
        activited = hero.skill1.activited;
        if (level > 0)
        {
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("HPup");
        }
        updateSkillInfo();
	}

    public void addSkill()
    {
        hero.Skill1LevelUp();
        


    }

    void updateSkillInfo()
    {
        this.transform.FindChild("Text").GetComponent<Text>().text = hero.skill1.level.ToString() + "/" + hero.skill1.maxLevel.ToString();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        myinfo = Instantiate(info);
        myinfo.transform.SetParent(this.transform.parent);
        myinfo.transform.localPosition = new Vector3(0, 300, 0);
        myinfo.transform.FindChild("info").GetComponent<Text>().text = information + "\n" + ll;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (myinfo != null)
            Destroy(myinfo);
    }

}
