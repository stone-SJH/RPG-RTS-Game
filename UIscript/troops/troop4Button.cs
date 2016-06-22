using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class troop4Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private GameObject info;
    public Troop troop4;

    // Use this for initialization
    void Start()
    {
        info = this.transform.FindChild("Info").gameObject;
        info.active = false;
        info.transform.FindChild("info").GetComponent<Text>().text = "咸鱼冲刺者\n" + "血量: " + troop4.maxHP + "\n" +
            "攻击: " + troop4.ordAttack + "\n" + "移动速度" + troop4.ordSpeed + "\n";
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
