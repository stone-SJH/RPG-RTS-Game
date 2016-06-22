using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class skill6Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Hero hero;
    private int level;
    private bool activited;
    private GameObject myinfo;
    public GameObject info;

    private string information;
    private string ll;


    // Use this for initialization
    void Start()
    {
        information = hero.skill6.introduction;
        ll = hero.skill6.GetAbility();
    }

    // Update is called once per frame
    void Update()
    {
        level = hero.skill6.level;
        activited = hero.skill6.activited;
        if (level > 0)
        {
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("cure");
        }
        updateSkillInfo();
    }

    public void addSkill()
    {
        hero.Skill6LevelUp();



    }

    void updateSkillInfo()
    {
        this.transform.FindChild("Text").GetComponent<Text>().text = hero.skill6.level.ToString() + "/" + hero.skill6.maxLevel.ToString();
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
