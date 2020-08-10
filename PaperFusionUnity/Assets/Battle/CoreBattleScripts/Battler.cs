using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Battler : MonoBehaviour
{ 
    /*
    This class is the abstract class for all fighters, virtual parameters, datafields and methods can be overwritten in concrete class implementations
    So far we have the player, and a base enemy class. We may also need a class for "fuse", or he may be cleverly combined in the base "player" class
    because you will never be in a fight without fuse (past the tutorial).
    */
    public virtual int health{get;set;} = 10;
    public virtual int maxHealth{get;set;} =10;
    protected BattleController battleController;
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
    {//called by the battleController. Links health depleted to flag for removal among other things
        this.battleController = battleController;
        OnHealthDepleted += battleController.handleHealthDepleted;
    }

    public virtual void playTurn()
    {//this script should give the player selections for his turn such as attack, item, runaway etc. 
        Debug.Log(this.name + " is playing his turn");
    }

    public virtual void getTargets()//List<Battler>
    {//This function requests targets from the battleController. The targets change based on targeting enemies, allies, all, flying, etc.
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
