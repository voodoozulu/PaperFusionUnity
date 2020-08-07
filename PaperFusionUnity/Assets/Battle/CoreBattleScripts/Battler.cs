using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Battler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void initialize();

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
        Debug.Log("Unchanged takeDamage Method");
    }
    public virtual void healDamage(Hit hit)
    {
        Debug.Log("Unchanged healDamage Method");
    }
}
