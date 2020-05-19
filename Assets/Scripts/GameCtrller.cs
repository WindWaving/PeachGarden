using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCtrller : MonoBehaviour
{
    private bool isGameOver;
    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //控制开始游戏后的UI
    private void OnGUI()
    {
        
    }

    //结束游戏
    public void onGameEnd()
    {
        isGameOver = true;
    }

    //保存背包到本地存储
    public void onSave()
    {

    }
}
