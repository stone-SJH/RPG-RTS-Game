﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class skill7Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
        information = hero.skill7.introduction;
        ll = hero.skill7.GetAbility();
    }

    // Update is called once per frame
    void Update()
    {
        level = hero.skill7.level;
        activited = hero.skill7.activited;
        if (level > 0)
        {
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>("guard");
        }
        updateSkillInfo();
    }

    public void addSkill()
    {
        hero.Skill7LevelUp();



    }

    void updateSkillInfo()
    {
        this.transform.FindChild("Text").GetComponent<Text>().text = hero.skill7.level.ToString() + "/" + hero.skill7.maxLevel.ToString();
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
