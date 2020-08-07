using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum StatusConditions{NONE,STUNNED, SLEEP,POISONED}
public class Hit
{
    public int damage;
    public int heal;
    public StatusConditions cond;
 
    // Start is called before the first frame update
    public Hit(int damage = 0, int heal = 0, StatusConditions cond = StatusConditions.NONE) //add condition enum for giving conditions, In this implementation, you can only have 1 status at a time like Pokemon
    {
        this.damage = damage;
        this.heal = heal;
        this.cond = cond;
    }
}
