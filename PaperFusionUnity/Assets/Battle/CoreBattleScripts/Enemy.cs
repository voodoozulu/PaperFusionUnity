﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Battler
{
    [SerializeField]
    private SOEnemyHealth enemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void initialize()
    {
        //any initialization goes in here
    }


}
