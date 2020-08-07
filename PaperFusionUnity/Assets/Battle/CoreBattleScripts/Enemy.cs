using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Battler
{
    public SOEnemyHealth sOEnemyHealth;
    [SerializeField]
    public int maxHealth{get; private set;}
    [SerializeField]
    private int _health;
    public int health
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
        
    }
    public override void initialize()
    {
        Debug.Log("badguy initialized");
        //any initialization goes in here
        maxHealth = sOEnemyHealth.maxHealth;
        health = maxHealth;
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
