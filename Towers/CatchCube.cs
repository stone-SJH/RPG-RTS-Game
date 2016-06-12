using UnityEngine;
using System.Collections;

public class CatchCube : MonoBehaviour {

    //使用显示轮廓的简单材质  
    public Material mSimpleMat;
    //使用显示轮廓的高级材质  
    public Material mAdvanceMat;
    //默认材质  
    public Material mDefaultMat;

    public Shader RimLightShader;
    public Color RimColor;
    //定义私有变量以存储模型的原始信息  
    private Renderer mSkin;
    private Color mColor;
    private Shader mShader;
    //是否被选中
    private bool Ifcatch;

    public string method="shader";

    void Start()
    {
        //RimColor = new Color(0, 0, 0, 0);
        //获取模型的SkinnedMeshRenderer  
        mSkin = this.transform.GetComponent<Renderer>();
        //获取默认颜色  
        mColor = mSkin.materials[0].color;
        //获取默认Shader  
        mShader = mSkin.materials[0].shader;
        Ifcatch = false;
    }

    void Update()
    {
        //获取鼠标位置  
        Vector3 mPos = Input.mousePosition;

        //向物体发射射线  
        Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit mHit;
        //射线检验  
        if (Physics.Raycast(mRay, out mHit))
        {
            //射线击中当前物体，表示鼠标指向该物体
            if (mHit.collider.gameObject.name == "Cube")
            {
                //更改shader方法
                if (method == "shader")
                {
                    mSkin.materials[0].shader = RimLightShader;
                    mSkin.materials[0].SetColor("_RimColor", RimColor);
                }
                else if (method == "material")
                {
                    //mSimpleMat.SetColor("_OutlineColor", RimColor);
                    mSkin.material = mSimpleMat;
                    //mHit.collider.gameObject.GetComponent<Renderer>().material = mSimpleMat;
                    //mSkin.material.SetColor("_OutlineColor", RimColor);
                    //mSkin.material.SetFloat("_Outline", 0.01f);
                }

                //鼠标左键点击选中
                if (Input.GetMouseButtonDown(0))
                {
                    Click();

                }
            }
            //射线未击中当前物体，表示鼠标未指向该物体
            else
            {
                if (Ifcatch == false)
                {
                    if (method == "shader")
                    {
                        mSkin.materials[0].shader = mShader;
                        mSkin.materials[0].SetColor("_RimColor", mColor);
                    }
                    else if (method == "material")
                    {
                        mSkin.material = mDefaultMat;
                       
                    }
                }
                //鼠标左键点击取消
                if (Input.GetMouseButtonDown(0))
                {
                    Cancel();
                }
            }
        }

    }
    //点击事件
    void Click()
    {
        Ifcatch = true;

    }
    void Cancel()
    {
        Ifcatch = false;

    }
}
