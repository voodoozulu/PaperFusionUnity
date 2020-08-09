using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Battler
{
    public SOEnemyHealth sOEnemyHealth;
    public int _health;
    public override int health
    { 
        get{return _health;}
        set
        {
            if (value > maxHealth)
            {
                _health = maxHealth;
            }
            else if (value < 0)
            {
                _health = 0;
            }
            else
            {
                _health = value;
            }
        } 

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {    
        if(Input.GetMouseButtonDown(0))
        takeDamage(new Hit(damage:1));
        if(Input.GetMouseButtonDown(1))
        healDamage(new Hit(heal:1));
    }
    public override void initialize(BattleController battleController)
    {
        Debug.Log("badguy initialized");
        //any initialization goes in here
        maxHealth = sOEnemyHealth.maxHealth;
        health = maxHealth;
        base.initialize(battleController);
    }




}
