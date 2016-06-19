using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class select : MonoBehaviour
{

    public Color rectColor = Color.green;
    static public ArrayList troops = new ArrayList();
    static public ArrayList selecttroops = new ArrayList();
    static public ArrayList notselecttroops = new ArrayList();

    private ArrayList deadtroops = new ArrayList();
    public Text txt;

    private Vector3 start = Vector3.zero;//记下鼠标按下位置
    private Vector3 end = Vector3.zero;//记下鼠标放开位置
    public  Material rectMat = null;//画线的材质 不设定系统会用当前材质画线 结果不可控
    private bool drawRectangle = false;//是否开始画线标志

    private bool chooseNode;

    public PathNode pn;

    public EventSystem eventsystem;
    public GraphicRaycaster graphicRaycaster;
    public Canvas ca;

    private GameModeSwitch gms;
    public Camera RTScamera;

    bool ifcatch;

    GameObject oobj;

    void Awake()
    {
        
        //RimColor = new Color(0.2F, 0.8F, 10.6F, 1);

        
        
        GameObject go1 = GameObject.Find("GameLogicManager");
        gms = go1.transform.GetComponent<GameModeSwitch>();
        GameObject go2 = GameObject.Find("RTSCamera");
        RTScamera = go2.transform.GetComponent<Camera>();
        
    }

    // Use this for initialization
    void Start()
    {
        //rectMat = new Material("Shader \"Lines/Colored Blended\" {" +
        //    "SubShader { Pass { " +
        //    "    Blend SrcAlpha OneMinusSrcAlpha " +
        //    "    ZWrite Off Cull Off Fog { Mode Off } " +
        //    "    BindChannels {" +
        //    "      Bind \"vertex\", vertex Bind \"color\", color }" +
        //    "} } }");//生成画线的材质

        rectMat.hideFlags = HideFlags.HideAndDontSave;
        rectMat.shader.hideFlags = HideFlags.HideAndDontSave;
        chooseNode = false;
        ifcatch = false;
    }


    void Update()
    {
        if (!chooseNode)
        {
            raycheck();
            
        }
        else
        {
            if (Input.GetMouseButtonDown(1))
            {
                foreach(GameObject obj in selecttroops)
                {
					if (obj != null){
                    	obj.GetComponent<Troop>().se.active = false;
					}
                }
                selecttroops.Clear();
                //notselecttroops.Clear();
                renderSelectcubes();
                chooseNode = false;
            }
        }
    }

    void OnPostRender()
    {//画线这种操作推荐在OnPostRender（）里进行 而不是直接放在Update，所以需要标志来开启
        if (drawRectangle)
        {
            end = Input.mousePosition;//鼠标当前位置
            GL.PushMatrix();//保存摄像机变换矩阵

            if (!rectMat)
                return;

            rectMat.SetPass(0);
            GL.LoadPixelMatrix();//设置用屏幕坐标绘图
            GL.Begin(GL.LINES);
            GL.Color(rectColor);//设置方框的边框颜色 边框不透明
            GL.Vertex3(start.x, start.y, 0);
            GL.Vertex3(end.x, start.y, 0);
            GL.Vertex3(end.x, start.y, 0);
            GL.Vertex3(end.x, end.y, 0);
            GL.Vertex3(end.x, end.y, 0);
            GL.Vertex3(start.x, end.y, 0);
            GL.Vertex3(start.x, end.y, 0);
            GL.Vertex3(start.x, start.y, 0);
            GL.End();
            GL.PopMatrix();//恢复摄像机投影矩阵
        }
    }

    void checkSelection(Vector3 start, Vector3 end)
    {
        Vector3 p1 = Vector3.zero;
        Vector3 p2 = Vector3.zero;
        if (start.x > end.x)
        {//这些判断是用来确保p1的xy坐标小于p2的xy坐标，因为画的框不见得就是左下到右上这个方向的
            p1.x = end.x;
            p2.x = start.x;
        }
        else
        {
            p1.x = start.x;
            p2.x = end.x;
        }
        if (start.y > end.y)
        {
            p1.y = end.y;
            p2.y = start.y;
        }
        else
        {
            p1.y = start.y;
            p2.y = end.y;
        }
        
        foreach (GameObject obj in troops)
        {//把可选择的对象保存在cubes数组里
            if (obj == null)
            {
                deadtroops.Add(obj);
                continue;
            }
            Vector3 location = this.GetComponent<Camera>().WorldToScreenPoint(obj.transform.position);//把对象的position转换成屏幕坐标
            if (location.x < p1.x || location.x > p2.x || location.y < p1.y || location.y > p2.y
                || location.z < this.GetComponent<Camera>().nearClipPlane || location.z > this.GetComponent<Camera>().farClipPlane)//z方向就用摄像机的设定值，看不见的也不需要选择了
            {
                disselecting(obj);//上面的条件是筛选 不在选择范围内的对象，然后进行取消选择操作，比如把物体放到default层，就不显示轮廓线了
            }
            else
            {
                selecting(obj);//否则就进行选中操作，比如把物体放到画轮廓线的层去
            }
        }
        foreach(GameObject obj in deadtroops)
        {
            troops.Remove(obj);
        }
        deadtroops.Clear();
    }

    void selecting(GameObject obj)
    {
        selecttroops.Add(obj);
    }
    void disselecting(GameObject obj)
    {
        //notselecttroops.Add(obj);
    }
    void renderSelectcubes()//渲染已选中的物体
    {
        foreach(GameObject obj in selecttroops)
        {
            obj.GetComponent<Troop>().se.active = true;
            //Debug.Log(obj.gameObject.name);
        }
        /*foreach (GameObject obj in notselecttroops)
        {
            obj.GetComponent<Troop>().se.active = false;
            //Debug.Log(obj.gameObject.name);
        }*/
        //txt.text = selecttroops.Count.ToString();
    }

    void raycheck()
    {
        Vector3 mPos = Input.mousePosition;

        if (CheckGuiRaycastObjects())
        {

            //deleteEffect();


            return;
        }
        if (gms.RTSmode)
        {
            //向物体发射射线  
            Ray mRay = RTScamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit mHit;
            //射线检验  
            if (Physics.Raycast(mRay, out mHit, Mathf.Infinity, 1 << 10))
            {
                //射线击中当前物体，表示鼠标指向该物体

                if (mHit.collider.gameObject.tag=="soldier")
                {
                    //更改shader方法
                    //makeEffect();


                    //鼠标左键点击选中
                    if (Input.GetMouseButtonDown(0))
                    {
                        //MakeInfo();

                        //makeEffect(mHit.collider.gameObject);
                        oobj = mHit.collider.gameObject;

                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        if (oobj == mHit.collider.gameObject)
                        {
                            makeEffect(oobj);
                        }
                    }
                }
                //射线未击中当前物体，表示鼠标未指向该物体
                else
                {

                    //deleteEffect();


                    //鼠标左键点击取消
                    if (Input.GetMouseButtonDown(0))
                    {

                    }
                }
            }
            //表示射线未击中任何物体
            else
            {


                //deleteEffect();


                //鼠标左键点击取消
                if (Input.GetMouseButtonDown(0))
                {
                    drawRectangle = true;//如果鼠标左键按下 设置开始画线标志
                    start = Input.mousePosition;//记录按下位置
                    selecttroops.Clear();
                    notselecttroops.Clear();
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    drawRectangle = false;//如果鼠标左键放开 结束画线
                    checkSelection(start, end);
                    renderSelectcubes();
                    if (selecttroops.Count != 0)
                    {
                        chooseNode = true;
                    }
                    GameObject.Find("PathNodes").GetComponent<RouteManager>().CreateNewRoute();
                    GameObject.Find("PathNodes").GetComponent<PathManager>().PathnodeAccessableCheck(pn);

                }
            }
        }
    }
    bool CheckGuiRaycastObjects()
    {
        PointerEventData eventData = new PointerEventData(eventsystem);
        eventData.pressPosition = Input.mousePosition;
        eventData.position = Input.mousePosition;

        List<RaycastResult> list = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, list);
        //Debug.Log(list.Count);
        return list.Count > 0;
    }

    void makeEffect(GameObject obj)
    {
        selecttroops.Clear();
        ifcatch = true;
        chooseNode = true;
        obj.GetComponent<Troop>().se.active = true;
        selecttroops.Add(obj);
        GameObject.Find("PathNodes").GetComponent<RouteManager>().CreateNewRoute();
        GameObject.Find("PathNodes").GetComponent<PathManager>().PathnodeAccessableCheck(pn);
    }
    void deleteEffect(GameObject obj)
    {
        ifcatch = false;
        chooseNode = false;
        obj.GetComponent<Troop>().se.active = false;
    }

}
