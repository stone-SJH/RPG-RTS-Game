using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class makeTroop : MonoBehaviour {


    public GameObject troop1;
    private GameObject mytroop1;

    public GameObject troop2;
    private GameObject mytroop2;

    public GameObject troop3;
    private GameObject mytroop3;

    public GameObject troop4;
    private GameObject mytroop4;

    public Vector2 start;
    public Vector2 end;
    public int rows;
    public int cols;
    public float y;

    static public Dictionary<Vector2, bool> positions = new Dictionary<Vector2, bool>();

    // Use this for initialization
    void Start () {
        float dx = (end.x - start.x) / (rows - 1);
        float dy = (end.y - start.y) / (cols - 1);
        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < cols; j++)
            {
                Vector2 p = new Vector2(start.x + j * dx, start.y + i * dy);
                positions.Add(p, false);
            }
        }
        
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void makeTroop1()
    {
        mytroop1 = Instantiate(troop1);
        foreach (KeyValuePair<Vector2, bool> po in positions)
        {
            if (po.Value == false)
            {
                mytroop1.transform.position = new Vector3(po.Key.x, y, po.Key.y);
                positions[po.Key] = true;
                break;
            }
        }
    }

    public void makeTroop2()
    {
        mytroop2 = Instantiate(troop2);
        foreach(KeyValuePair<Vector2,bool> po in positions)
        {
            if (po.Value == false)
            {
                mytroop2.transform.position = new Vector3(po.Key.x, y, po.Key.y);
                positions[po.Key] = true;
                break;
            }
        }
    }

    public void makeTroop3()
    {
        mytroop3 = Instantiate(troop3);
        foreach (KeyValuePair<Vector2, bool> po in positions)
        {
            if (po.Value == false)
            {
                mytroop3.transform.position = new Vector3(po.Key.x, y, po.Key.y);
                positions[po.Key] = true;
                break;
            }
        }

    }

    public void makeTroop4()
    {
        mytroop4 = Instantiate(troop4);
        foreach (KeyValuePair<Vector2, bool> po in positions)
        {
            if (po.Value == false)
            {
                mytroop4.transform.position = new Vector3(po.Key.x, y, po.Key.y);
                positions[po.Key] = true;
                break;
            }
        }
    }


}
