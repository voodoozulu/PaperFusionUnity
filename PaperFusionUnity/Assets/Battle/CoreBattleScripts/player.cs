using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Battler
{
    public SOPlayerHealth sOPlayerHealth;
    private int health{get => sOPlayerHealth.health; set => sOPlayerHealth.health = value;} 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void initialize()
    {
        //any initialization goes in here
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            takeDamage(new Hit(damage:1));
        if(Input.GetMouseButtonDown(1))
            healDamage(new Hit(heal:1));
    }
        public override void takeDamage(Hit hit)
    {
        health -= hit.damage;
        //TODO add condition for dying
    }

        public override void healDamage(Hit hit)
    {
        health += hit.heal;
        //TODO add condition for dying
    }
}
