using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class inputCount : MonoBehaviour {

    public Item it;
    private string curString;
    private int curNum=0;

	// Use this for initialization
	void Start () {
        
        this.GetComponent<InputField>().characterValidation = InputField.CharacterValidation.Integer;
        this.GetComponent<InputField>().text = curNum.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void checkNumber(string s)
    {
        curString = this.GetComponent<InputField>().text;
        Debug.Log(curString);
        /*if (int.Parse(curString) > it.itemNumber)
        {
            this.transform.FindChild("Text").GetComponent<Text>().text = it.itemNumber.ToString();
            curNum = it.itemNumber;
        }
        */
    }

    public void add()
    {
        curNum++;
        this.GetComponent<InputField>().text = curNum.ToString();

    }

    public void minus()
    {
        if(curNum>0)
            curNum--;
        this.GetComponent<InputField>().text = curNum.ToString();

    }

    public void minusAll()
    {
        curNum = 0;
        this.GetComponent<InputField>().text = curNum.ToString();
    }

    public void addAll()
    {
        curNum = it.itemNumber;
        this.GetComponent<InputField>().text = curNum.ToString();
    }


    public void sell()
    {

    }

}
