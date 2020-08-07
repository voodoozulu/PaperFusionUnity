using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Health", menuName = "Stats/PlayerHealth")]
public class SOPlayerHealth : SOHealth
{
    
    [SerializeField]
    private int _health;
    public int health
    {
        get{return _health;}
        set
        {
            if (value > maxhealth)
            {
                _health = maxhealth;
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
    
    void Awake()
    {
     maxhealth = 10;
     health = maxhealth;
    }
}

