using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Props : MonoBehaviour
{
    private string name;//道具名称
    private string description;//道具描述
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //显示道具描述信息
    public void onShowDesc()
    {

    }

    //触发使用事件
    public abstract void onUseTrigger();
}
