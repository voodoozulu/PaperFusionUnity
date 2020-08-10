using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Stats/Health")]
public class PlayerHealth : ScriptableObject
{
    public int maxhealth;
    
    [SerializeField]
    private int _health; //backing field
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

