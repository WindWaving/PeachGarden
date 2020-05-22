using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_old:MonoBehaviour
{
    public float visualRange;//可视范围
    public float patrolSpeed;//敌人速度
    public float chaseSpeed;//追击速度
    public bool canPatrol;//是否会巡逻1
    public float patrolDis;//巡逻最远距离
    public float chaseDis;//追击最远距离

    public float rotSpeed = 5f;//转弯的速度
    private bool isChasing=false;//是否正在追击
    private NavMeshAgent agent;//导航代理
    private Vector3 initPos;//初始位置
    public float sightAngle = 60f;
    private GameObject player;
    private GameCtrller gameCtrller;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        gameCtrller= GameObject.Find("gameCtrller").GetComponent<GameCtrller>();
        agent = GetComponent<NavMeshAgent>();
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float dis = Vector3.Distance(player.transform.position, transform.position);
        print(dis);
        if (isPlayerIn())
        {
            onChase();
        }
        else
        {
            onPatrol();
        }
    }


    //巡逻
    public void onPatrol()
    {
        //能够巡逻的敌人会向前走动
        if (canPatrol)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * patrolSpeed);
            float disToInit = Vector3.Distance(transform.position, initPos);
            if (disToInit > patrolDis)
            {
                Quaternion backRot = Quaternion.LookRotation(initPos - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, backRot, rotSpeed);
            }
        }
        //不会巡逻的敌人待在原地
        else
        {
            
        }
    }

    //追击
    private void onChase()
    {
        gameCtrller.setChasing();
        agent.SetDestination(player.transform.position);
        float disToInit = Vector3.Distance(transform.position, initPos);
        if (disToInit > chaseDis)
        {
            agent.SetDestination(initPos);
            gameCtrller.notChasing();
            //Quaternion backRot = Quaternion.LookRotation(initPos - transform.position);
            //transform.rotation = Quaternion.Slerp(transform.rotation, backRot, rotSpeed);
        }
    }

    //检测是否发现玩家
    private bool isPlayerIn()
    {
        Vector3 dir = player.transform.position - transform.position;
        float angle = UnityEngine.Vector3.Angle(dir, transform.forward);
        //在一定角度内一定距离内发现玩家
        if (angle < sightAngle)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir,out hit,visualRange))
            {
                if (hit.collider.tag == "Player")
                {
                    return true;
                }
            }
        }
        return false;
    }


}
