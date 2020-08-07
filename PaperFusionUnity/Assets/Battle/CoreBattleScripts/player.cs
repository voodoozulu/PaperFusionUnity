using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Battler
{
    [SerializeField]
    private SOPlayerHealth playerHealth;
    public 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void initialize()
    {
        //any initialization goes in here
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            playerHealth.health--;
        if(Input.GetMouseButtonDown(1))
            playerHealth.health++;
    }
}
