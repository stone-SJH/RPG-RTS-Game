using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class skill5Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
        level = hero.skill5.level;
        activited = hero.skill5.activited;
        if (level > 0)
        {
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("speedaura");
        }
        updateSkillInfo();
    }

    public void addSkill()
    {
        hero.Skill5LevelUp();



    }

    void updateSkillInfo()
    {
        this.transform.FindChild("Text").GetComponent<Text>().text = hero.skill5.level.ToString() + "/" + hero.skill5.maxLevel.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
