using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Battler : MonoBehaviour
{ 
    public virtual int health{get;set;} = 10;
    public virtual int maxHealth{get;set;} =10;
    private BattleController battleController;
    public Action<int,int> OnHealthChanged = delegate { };
    public Action<Battler> OnHealthDepleted = delegate {};
    public bool isPlayable = true;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void initialize(BattleController battleController)
    {
        this.battleController = battleController;
        OnHealthDepleted += battleController.handleHealthDepleted;
    }

    public virtual void playTurn()
    {
        Debug.Log(this.name + " is playing his turn");
    }

    public virtual void getTargets()//List<Battler>
    {
        Debug.Log(this.name + "Is getting targets, This virtual function hasn't been overridden");
    }

    public virtual void takeDamage(Hit hit)
    {
        health -= hit.damage;
        //TODO add condition for dying
        OnHealthChanged(health, maxHealth);
        if(health == 0) OnHealthDepleted(this);
    }
    public virtual void healDamage(Hit hit)
    {
        health += hit.heal;
        OnHealthChanged(health, maxHealth);
    }

}
