using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Health", menuName = "Stats/PlayerHealth")]
public class SOPlayerHealth : ScriptableObject
{
    public int maxHealth;
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
    
    void Awake()
    {
     maxHealth = 10;
     health = maxHealth;
    }
}

