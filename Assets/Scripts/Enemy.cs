using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float visualRange;//可视范围
    public float patrolSpeed;//敌人速度
    public float chaseSpeed;//追击速度
    public bool canPatrol;//是否会巡逻
    public float patrolDis;//巡逻最远距离
    public float chaseDis;//追击最远距离
    public bool isPatroling = true;//是否正在巡逻
    public bool isChasing = false;//是否正在追击
    public bool isBacking = false;//是否正在返回原点

    private float rotSpeed = 5f;//转弯的速度
    private NavMeshAgent agent;//导航代理
    private Animator animator;//动画组件
    private Vector3 initPos;//初始位置
    private Quaternion initRot;//初始角度
    private float sightAngle = 60f;//可检测的角度范围

    public GameObject player;
    private GameCtrller gameCtrller;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        //player = (GameObject)Resources.Load("Player");
        gameCtrller = GameObject.Find("gameCtrller").GetComponent<GameCtrller>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        initPos = transform.position;
        initRot = transform.rotation;
        //this.visualRange = 5f;
        //this.chaseSpeed = 3f;
        //this.canPatrol = false;
        //this.chaseDis = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerIn();
        if (isChasing)
        {
            onChase();
        }
        
        if (isBacking)
        {
            goBack();
        }

        if(isPatroling)
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
            animator.Play("Run");
            transform.Translate(Vector3.forward * Time.deltaTime * patrolSpeed);
            float disToInit = Vector3.Distance(transform.position, initPos);
            if (disToInit > patrolDis||isObstacle())
            {
                Quaternion backRot = Quaternion.LookRotation(initPos - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, backRot, rotSpeed);
            }
        }
        //不会巡逻的敌人待在原地
        else
        {
            //animator.Play("Idle");
        }
    }

    private bool isObstacle()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,Vector3.forward,out hit, 3f))
        {
            //如果撞到的不是玩家或者敌人，则是障碍物
            if (hit.collider.tag != "Player"&& hit.collider.tag!="Enemy")
            {
                return true;
            }
        }
        return false;
    }

    //追击
    public void onChase()
    {
        isPatroling = false;
        isBacking = false;
        isChasing = true;
        float disToInit = Vector3.Distance(transform.position, initPos);
        if (disToInit > chaseDis)
        {
            isPatroling = false;
            isChasing = false;
            isBacking = true;
        }
        animator.Play("Run");
        //string animString = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        //print(animString);
        gameCtrller.setChasing();
        agent.SetDestination(player.transform.position);
        
    }

    public void goBack()
    {
        isChasing = false;
        isPatroling = false;
        animator.Play("Run");
        agent.SetDestination(initPos);
        gameCtrller.notChasing();
        if (Vector3.Distance(transform.position,initPos)<3f)
        {
            isBacking = false;
            isChasing = false;
            isPatroling = true;
            transform.rotation = Quaternion.Slerp(transform.rotation, initRot, rotSpeed);
        }
    }

    //检测是否发现玩家
    private bool isPlayerIn()
    {
        if (!player)
        {
            return false;
        }
        //只有在原地的时候才检测玩家
        if (!isChasing&&isPatroling&&!isBacking)
        {
            Vector3 dir = player.transform.position - transform.position;
            float angle = UnityEngine.Vector3.Angle(dir, transform.forward);
            //在一定角度内一定距离内发现玩家
            if (angle < sightAngle)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, dir, out hit, visualRange))
                {
                    if (hit.collider.tag == "Player")
                    {
                        isChasing = true;
                        isPatroling = false;
                        isBacking = false;
                        return true;
                    }
                }
            }
        }
        return false;
    }

}
