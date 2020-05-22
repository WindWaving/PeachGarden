using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCtrller : MonoBehaviour
{
    private bool isGameOver=false;
    private bool isFailedWin=false;//失败
    private bool isNextWin = false;//下一关
    private string[] scenes;//所有关卡场景
    private int curScId;//当前场景的id

    //敌人预设
    private GameObject servant;
    private GameObject guard;
    private GameObject pharmacist;
    //玩家预设
    private GameObject player;

    public Text tips;

    private JsonOp jsonOp;
    //public GameObject fastEnemy;

    private void Awake()
    {
        servant = (GameObject)Resources.Load("servant");
        guard = (GameObject)Resources.Load("guard");
        pharmacist = (GameObject)Resources.Load("pharmacist");
        //初始化敌人
        int num = UnityEngine.Random.Range(3, 6);
        Vector3 center1 = new Vector3(10, 1, 10);
        onShowEnemy(servant, num, center1);

        Vector3 center2 = new Vector3(14, 0, 2);
        num = UnityEngine.Random.Range(1, 4);
        onShowEnemy(guard, num, center2);

        Vector3 center3 = new Vector3(8, 1, 15);
        num = UnityEngine.Random.Range(2, 5);
        onShowEnemy(pharmacist, num, center3);

        //初始化玩家
        player = (GameObject)Resources.Load("player_female");
        onShowPlayer();

        jsonOp = new JsonOp();
    }
    // Start is called before the first frame update
    void Start()
    {
        scenes = new string[4] { "level1","level2","level3","level4" };
        //获取当前场景的编号
        string sceneName = SceneManager.GetActiveScene().name;
        curScId=scenes.ToList().IndexOf(sceneName);
    }

    //控制开始游戏后的UI
    private void OnGUI()
    {
        //背包和设置
        if(GUI.Button(new Rect(Screen.width - 80, 10, 60, 30), "背包"))
        {
            print("bag");
        }
        if(GUI.Button(new Rect(Screen.width - 80, 50, 60, 30), "设置"))
        {
            print("setting");
        }
        if(GUI.Button(new Rect(Screen.width - 80, 90, 60, 30), "退出"))
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }

        //游戏结束的UI
        if (isGameOver&&isFailedWin)
        {
             GUI.Window(0, new Rect(Screen.width / 2 - 150, Screen.height / 2 - 100, 300, 200), endWindow, "你失败了");
            
        }
        if (isGameOver && isNextWin)
        {
            GUI.Window(1, new Rect(Screen.width / 2 - 150, Screen.height / 2 - 100, 300, 200), nextWindow, "成功过关");
        }
    }

    //随机位置和角度生成敌人
    private void onShowEnemy(GameObject enemy,int num,Vector3 center)
    {
        for (int i = 1; i <= num; ++i)
        {
            float range = UnityEngine.Random.Range(-2, 2);
            float angle = UnityEngine.Random.Range(0, 360);
            Quaternion rot = Quaternion.Euler(new Vector3(0, angle + UnityEngine.Random.Range(-20, 20), 0));
            Instantiate(enemy, new Vector3(center.x + range, center.y, center.z + range), rot);
        }
    }

    //固定位置生成玩家
    private void onShowPlayer()
    {
        Quaternion rot = Quaternion.Euler(new Vector3(0, 90, 0));
        Instantiate(player, new Vector3(1, 0, 1), rot);
    }
    private void endWindow(int winId)
    {
        if(GUI.Button(new Rect(100, 50, 100, 40), "退出"))
        {
            isFailedWin = false;
            onSave();//保存数据
            //加载关卡选择的场景

        }
        if (GUI.Button(new Rect(100, 120, 100, 40), "重新开始"))
        {
            EditorSceneManager.LoadScene(scenes[curScId]);
            //清空保存的数据
            jsonOp.clearFile();
        }
    }
    private void nextWindow(int winId)
    {
        if (GUI.Button(new Rect(150, 100, 100, 50), "恭喜你通关了，拜拜"))
        {
            isFailedWin = false;
            //加载关卡选择的场景

        }
        if (curScId < scenes.Length - 1)
        {
            if(GUI.Button(new Rect(10, 100, 80, 50), "下一关"))
            {
                EditorSceneManager.LoadScene(scenes[++curScId]);
            }
        }
    }


    //结束游戏
    public void onGameEnd()
    {
        isGameOver = true;
        isFailedWin = true;
        tips.text = "你被抓了，闯关失败";
    }

    //保存数据到本地存储
    public void onSave()
    {
        
    }
    //正在被追的文本
    public void setChasing()
    {
        tips.text = "有敌人盯上你了，快跑！";
    }

    public void notChasing()
    {
        tips.text = "脱离了危险，小命保住了！";
    }
}
