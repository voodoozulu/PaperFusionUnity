using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CombatUiButtonMovement : MonoBehaviour
{
    [Header("Path Settings")]
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    private float _speed;
    float distanceTravelled;
    private bool coroutine = false;
    private Coroutine anim;
    private float top;
    private float bottom;
    private Vector3 initialScale;
    
    [Header("index Settings")]
    public int index;
    public int indexTotal;
    public List<float> posList;

    [Header("Button Settings")] 
    private Button button;
    [SerializeField] private SOSkill soskill;
    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        _speed = speed;
        top = pathCreator.path.GetPointAtTime(0.25f).y;
        bottom = pathCreator.path.GetPointAtTime(0.75f).y;
        initialScale = transform.localScale;
        for (float i = 0; i < indexTotal; i++)
        {
            posList.Add(i/indexTotal + 0.75f);
        }
        
        transform.position = pathCreator.path.GetPointAtTime(posList[index]);
        changeScale(posList[index]);
        isFocused(index);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up") && !coroutine)
        {
            coroutine = true;
            anim = StartCoroutine(animatethebuttonRight(index));
        }
        
        if (Input.GetKey("down") && !coroutine)
        {
            coroutine = true;
            anim = StartCoroutine(animatethebuttonLeft(index));
        }

    }

    private IEnumerator animatethebuttonRight(int ind)
    {
        coroutine = true;
        int indOld = ind;

        if (ind == 2)
        {
            ind = (ind - 2) % indexTotal;
            speed = speed * 2;
        }
        else if (ind == 0)
        {
            ind = indexTotal - 2;
            speed = speed * 2;
        }
        else
        {
            ind--;
        }
        float told = posList[indOld];
        float t    = posList[ind];
        if (told < t) told++;
        while (told > t )
        {
            told -= speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtTime(told, endOfPathInstruction);
            changeScale(told);
            yield return null;
        }
        transform.position = pathCreator.path.GetPointAtTime(t, endOfPathInstruction);
        changeScale(t);
        coroutine = false;
        speed = _speed;
        index = ind;
        isFocused(index);
    }
    
    private IEnumerator animatethebuttonLeft(int ind)
    {
        coroutine = true;
        int indOld = ind;

        if (ind == indexTotal - 2)
        {
            ind = 0;
            speed = speed * 2;
        }
        else if (ind == 0)
        {
            ind = 2;
            speed = speed * 2;
        }
        else
        {
            ind++;
        }
        float told = posList[indOld];
        float t    = posList[ind];
        if (told > t) told--;
        while (told < t )
        {
            told += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtTime(told, endOfPathInstruction);
            changeScale(told);
            yield return null;
        }
        transform.position = pathCreator.path.GetPointAtTime(t, endOfPathInstruction);
        changeScale(t);
        coroutine = false;
        speed = _speed;
        index = ind;
        isFocused(index);
    }

    private void changeScale(float told)
    {
        transform.localScale =
            initialScale * Mathf.Lerp(1f, .5f, Mathf.InverseLerp(bottom, top, transform.position.y));
    }

    private void isFocused(int ind)
    {
        if (ind == 0)
        {
            button.interactable = true;
            //button.onClick.RemoveAllListeners();
            //button.onClick.AddListener(testFunc); // opens menu dialogue based on button 
        }
        else
        {
            button.interactable = false;
            //button.onClick.RemoveAllListeners();
            //button.onClick.AddListener(moveToStart);
        }
    }
    
    private void moveToStart()
    {// this function is *supposed* to move every button. It doesn't work so it is unused for now. 
        StartCoroutine(iEnumMoveToStart());
    }

    private IEnumerator iEnumMoveToStart()
    {
        while (index != 0)
        {
            if (index < indexTotal / 2 && !coroutine)
            {
                foreach (CombatUiButtonMovement child in gameObject.transform.parent.GetComponentsInChildren<CombatUiButtonMovement>())
                {
                    child.StartCoroutine(animatethebuttonRight(child.index));
                }
                
            }
            if (index >= indexTotal / 2 && !coroutine)
            {
                foreach (CombatUiButtonMovement child in gameObject.transform.parent.GetComponentsInChildren<CombatUiButtonMovement>())
                {
                    child.StartCoroutine(animatethebuttonLeft(child.index));
                }
            }
            yield return null;
        }
    }

    private void testFunc()
    {
        Debug.Log(gameObject.transform.parent.GetComponentsInChildren<CombatUiButtonMovement>());
    }
}
