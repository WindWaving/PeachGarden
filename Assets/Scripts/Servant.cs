using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Servant : Enemy
{
    // Start is called before the first frame update
    private void Start()
    {
        this.visualRange = 5f;
        this.chaseSpeed = 3f;
        this.canPatrol = false;
        this.chaseDis = 10f;

        this.player = GameObject.FindWithTag("Player");

    }

    
}
