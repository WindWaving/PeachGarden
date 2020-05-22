using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private Transform player;
    private Vector3 offsetPosition;
    private float distance;//在缩放视角时的offset变化量
    private float updis = 1f;
    private float scrollSpeed = 10; //鼠标滚轮速度
    private bool isRotating; //开启摄像机旋转
    public float rotateSpeed = 10; //摄像机旋转速度
    private int rotFlag = 0;//默认是鼠标旋转

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        //获取摄像机与player的位置偏移
        offsetPosition = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        //摄像机跟随player与player保持相对位置偏移 
        transform.position = offsetPosition + player.position;
        //摄像机的旋转
        RotateView();
        //视角大小
        ScrollView();
    }

    void ScrollView()
    {
        //返回位置偏移的向量长度
        distance = offsetPosition.magnitude;

        //根据鼠标滚轮的前后移动获取变化长度
        distance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;

        //限制变化长度的范围在最小为4最大为22之间
        distance = Mathf.Clamp(distance, 4, 22);

        //新的偏移值为偏移值的单位向量*变换长度
        offsetPosition = offsetPosition.normalized * distance;

    }

    void RotateView()
    {
        //按下鼠标左键开启旋转摄像机
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
            rotFlag = 0;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            isRotating = true;
            rotFlag = -1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            isRotating = true;
            rotFlag = 1;
        }

        //抬起鼠标左键关闭旋转摄像机
        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            //获取摄像机初始位置
            Vector3 pos = transform.position;
            //获取摄像机初始角度
            Quaternion rot = transform.rotation;
            if (rotFlag == 0)
            {
                //摄像机围绕player的位置延player的y轴旋转,旋转的速度为鼠标水平滑动的速度
                transform.RotateAround(player.position, player.up, Input.GetAxis("Mouse X") * rotateSpeed);

                //摄像机围绕player的位置延自身的x轴旋转,旋转的速度为鼠标垂直滑动的速度
                transform.RotateAround(player.position, player.right, Input.GetAxis("Mouse Y") * rotateSpeed);

                //获取摄像机x轴向的欧拉角
                float x = transform.eulerAngles.x;

                ////如果摄像机的x轴旋转角度超出范围,恢复初始位置和角度
                //if (x < 10 || x > 80)
                //{
                //    transform.position = pos;
                //    transform.rotation = rot;
                //}
            }
            else
            {
                transform.RotateAround(player.position, player.up, rotFlag * rotateSpeed);
            }
            offsetPosition = transform.position - player.position;

        }


    }
}
