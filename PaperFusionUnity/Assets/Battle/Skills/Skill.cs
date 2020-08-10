using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public int damage = 0;
    public int bonus = 1; 
    public int maxDamage = 3;
    public event Action initialize = delegate { };
    public event Action startEvent = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        initialize();
        StartCoroutine(waitSomeTime(2f));
    }

    protected IEnumerator waitSomeTime(float time)
    {
        yield return new WaitForSeconds(time);
        startEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDisable()
    {

    }
}
