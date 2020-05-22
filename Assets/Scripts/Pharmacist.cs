using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pharmacist : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        this.chaseSpeed = 4f;
        this.visualRange = 8f;
        this.canPatrol = false;
        this.chaseDis = 10f;
        this.player = GameObject.FindWithTag("Player");
    }

}
