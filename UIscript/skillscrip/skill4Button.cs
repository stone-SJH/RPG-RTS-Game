using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class skill4Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
        level = hero.skill4.level;
        activited = hero.skill4.activited;
        if (level > 0)
        {
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("HPaura");
        }
        updateSkillInfo();
    }

    public void addSkill()
    {
        hero.Skill4LevelUp();



    }

    void updateSkillInfo()
    {
        this.transform.FindChild("Text").GetComponent<Text>().text = hero.skill4.level.ToString() + "/" + hero.skill4.maxLevel.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
