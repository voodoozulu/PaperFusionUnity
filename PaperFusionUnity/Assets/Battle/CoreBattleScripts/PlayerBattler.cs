﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattler : Battler
{
    public SOPlayerHealth sOPlayerHealth;
    public SOSkillsKnown skillsKnown;
    private List<GameObject> skills = new List<GameObject>();

    public override int health{get => sOPlayerHealth.health; set => sOPlayerHealth.health = value;} 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void initialize(BattleController battleController)
    {
        base.initialize(battleController);

        //populate known skills
        foreach(GameObject skill in skillsKnown.skillsKnown)
        {
            skills.Add(Instantiate(skill, gameObject.transform));
        }
    }

    // Update is called once per frame
    void Update()
    { //deals damage or heals on mouse click for testing. See health changes in inspector
        if(Input.GetMouseButtonDown(0))
            takeDamage(new Hit(damage:1));
        if(Input.GetMouseButtonDown(1))
            healDamage(new Hit(heal:1));
    }
        public override void takeDamage(Hit hit)
    {
        health -= hit.damage;
        
    }//TODO add condition for dying

        public override void healDamage(Hit hit)
    {
        health += hit.heal;
        //TODO add condition for dying
    }
}
