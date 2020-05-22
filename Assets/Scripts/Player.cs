using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 5f;

    private Animator animator;
    private GameCtrller gameCtrller;
    // Start is called before the first frame update
    private void Awake()
    {
        gameCtrller = GameObject.Find("gameCtrller").GetComponent<GameCtrller>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        walkSpeed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        onWalk();
        onJump();
    }

    private void onWalk()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.Play("Walk");
            //animator.SetBool("Walk", true);
            //string animString = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            //print(animString);
            print("next");
            print(Vector3.forward * Time.deltaTime * walkSpeed);
            print(walkSpeed);
            print(Vector3.forward );
            print(Time.deltaTime);
            transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            //["Walk"]
            animator.Play("Attack_stance");
        }
    }
    private void onJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //animator.Play("Jump");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            gameCtrller.onGameEnd();
        }
    }
}
