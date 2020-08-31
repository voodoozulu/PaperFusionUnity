using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class CombatUiButtonMovement : MonoBehaviour
{
    [Header("Path Settings")]
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    float distanceTravelled;
    public float tstart;
    private bool coroutine = false;
    private Coroutine anim;
    private float t = 0f;
    private float top;
    private float bottom;
    private Vector3 initialScale;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = pathCreator.path.GetPointAtTime(tstart);
        t = tstart;
        top = pathCreator.path.GetPointAtTime(0.25f).y;
        bottom = pathCreator.path.GetPointAtTime(0.75f).y;
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up") && !coroutine)
        {
            coroutine = true;
            float told = t;
            t += 0.25f;
            anim = StartCoroutine(animatethebutton(told,t));
        }
        
        if (Input.GetKey("down") && !coroutine)
        {
            coroutine = true;
            float told = t;
            t -= 0.25f;
            anim = StartCoroutine(animatethebutton(told,t));
        }

    }

    private IEnumerator animatethebutton(float told, float t)
    {
        while (told > t)
        {
            told -= speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtTime(told, endOfPathInstruction);
            changeScale(told);
            yield return null;
        }
        
        while (told < t)
        {
            told += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtTime(told, endOfPathInstruction);
            changeScale(told);
            yield return null;
        }

        transform.position = pathCreator.path.GetPointAtTime(t, endOfPathInstruction);
        changeScale(told);
        coroutine = false;
    }

    private void changeScale(float told)
    {
        transform.localScale =
            initialScale * Mathf.Lerp(1f, .5f, Mathf.InverseLerp(bottom, top, transform.position.y));
    }
}
