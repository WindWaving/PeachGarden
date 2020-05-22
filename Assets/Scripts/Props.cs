using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Props : MonoBehaviour
{
    private string propName="回魂丹";//道具名称
    private string description="可以让人死而复生";//道具描述

    private Shader lightShader;//发光的shader
    private Shader initShader;//原始的shader

    public Text tips;//文本显示区
    // Start is called before the first frame update
    void Start()
    {
        initShader = GetComponent<Renderer>().material.shader;
        lightShader = Shader.Find("Custom/borderLight");
    }


    //鼠标悬浮在物品上高亮边缘
    private void OnMouseOver()
    {
        GetComponent<Renderer>().material.shader = lightShader;
        //左键显示物品信息，右键将物品放入背包
        if (Input.GetMouseButtonDown(0))
        {
            tips.text = propName + " ：" + description;
        }
        if (Input.GetMouseButtonDown(1))
        {
            
        }
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.shader = initShader;
    }

}
