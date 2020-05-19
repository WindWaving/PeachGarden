using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public GameCtrller gameCtrl;//todo:需要拖入对象
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //检测游戏结束
    private void OnTriggerEnter(Collider other)
    {
        //检测和敌人的碰撞


        /**/
        gameCtrl.onGameEnd();
    }
}
