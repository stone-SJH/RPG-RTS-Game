using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class skill8Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
        level = hero.skill8.level;
        activited = hero.skill8.activited;
        if (level > 0)
        {
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("burst");
        }
        updateSkillInfo();
    }

    public void addSkill()
    {
        hero.Skill8LevelUp();



    }

    void updateSkillInfo()
    {
        this.transform.FindChild("Text").GetComponent<Text>().text = hero.skill8.level.ToString() + "/" + hero.skill8.maxLevel.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

}
