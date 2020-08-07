using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Battler : MonoBehaviour
{
    public SOHealth health; 
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

    public void getTargets()//List<Battler>
    {

    }
}
