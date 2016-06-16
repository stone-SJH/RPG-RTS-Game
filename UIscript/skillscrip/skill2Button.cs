using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class skill2Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Hero hero;
    private int level;
    private bool activited;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        level = hero.skill2.level;
        activited = hero.skill2.activited;
        if (level > 0)
        {
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("attackup");
        }
        updateSkillInfo();
    }

    public void addSkill()
    {
        hero.Skill2LevelUp();



    }

    void updateSkillInfo()
    {
        this.transform.FindChild("Text").GetComponent<Text>().text = hero.skill2.level.ToString() + "/" + hero.skill2.maxLevel.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)

    {

    }

}
