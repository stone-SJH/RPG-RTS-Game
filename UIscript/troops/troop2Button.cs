using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class troop2Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private GameObject info;
    public Troop troop2;

    // Use this for initialization
    void Start()
    {
        info = this.transform.FindChild("Info").gameObject;
        info.active = false;
        info.transform.FindChild("info").GetComponent<Text>().text = "兽族打击者\n" + "血量: " + troop2.maxHP + "\n" +
            "攻击: " + troop2.ordAttack + "\n" + "移动速度" + troop2.ordSpeed + "\n";
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        info.active = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        info.active = false;
    }


}
