using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class select : MonoBehaviour
{

    public Color rectColor = Color.green;
    static public ArrayList cubes = new ArrayList();
    static public ArrayList selectcubes = new ArrayList();
    public Text txt;

    private Vector3 start = Vector3.zero;//记下鼠标按下位置
    private Vector3 end = Vector3.zero;//记下鼠标放开位置
    public  Material rectMat = null;//画线的材质 不设定系统会用当前材质画线 结果不可控
    private bool drawRectangle = false;//是否开始画线标志
   

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
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            drawRectangle = true;//如果鼠标左键按下 设置开始画线标志
            start = Input.mousePosition;//记录按下位置
            selectcubes.Clear();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            drawRectangle = false;//如果鼠标左键放开 结束画线
            checkSelection(start,end);
            renderSelectcubes();
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
        foreach (GameObject obj in cubes)
        {//把可选择的对象保存在cubes数组里
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
    }

    void selecting(GameObject obj)
    {
        selectcubes.Add(obj);
    }
    void disselecting(GameObject obj)
    {

    }
    void renderSelectcubes()//渲染已选中的物体
    {
        txt.text = selectcubes.Count.ToString();
    }
}
