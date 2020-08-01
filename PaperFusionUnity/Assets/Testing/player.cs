using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth playerHealth;
    public 
    // Start is called before the first frame update
    void Start()
    {
        
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
