using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        this.visualRange = 10f;
        this.patrolSpeed = 3f;
        this.chaseSpeed = 6f;
        this.canPatrol = true;
        this.patrolDis = 15f;
        this.chaseDis = 6f;
        this.player = GameObject.FindWithTag("Player");
    }

}
