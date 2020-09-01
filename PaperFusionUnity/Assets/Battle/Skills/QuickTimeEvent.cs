using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
This class is a template for how I will be creating the Abstract QTE. Everything here is temporary. 
the exact QTE works like this:

The class starts disabled, upon waking (The step when the script is compiled but before it processes anything inside other than the Awake() function)
it subscribes to the attached Skill class (different script). This script will listen to the Skill class to enable. 

Upon enable a random number is generated (QTEGen) in the Update method and starts a two timer coroutines. The
first timer coroutine totalCountDown() is the total time the QTE will run. After its time is up, the QTE ends
The second timer is the timer for an individual key stroke. 

In Update() the script listens for a key press. No matter what key is pressed the QTE starts keyPressing() coroutine and changes the correct flag
In the keyPressing() Coroutine 

If countDown() stops it is counted as an incorrect answer, new QTEgen is produced and timer is reset

keyPressing() stops countDown(), registers a point for the player if it succeeds (the points are damage), and resets timer if incorrect. 

If totalCountDown() finishes, or if the player has scored the maximum points, all coroutines stop an event is fired letting the Skill class
 know the QTE is finished and this script is disabled. 
*/

public class QuickTimeEvent : MonoBehaviour
{
    private Skill skill;
    
    public float difficulty = 0.75f;
    private float _difficulty; //resets difficulty value on re-enable
    public float difficultyStep = 0.1f;
    private float _difficultyStep;//resets difficultyStep value on re-enable
    public float totalTime = 7; //total QTE time
    public GameObject displayBox;
    public GameObject passBox;
    public Image timer;
    private int QTEGen, waitingForKey, correctKey, countingDown; //for logic of QTE feature
    Coroutine countDownCoroutine = null;
    Coroutine keyPressingCoroutine = null;

    public event Action onQTEComplete = delegate {};


    void Awake()
    {
        _difficulty = difficulty;
        _difficultyStep = difficultyStep;
        skill = gameObject.GetComponent(typeof(Skill)) as Skill;
        skill.initialize += initialize;
        skill.startEvent += startEvent; 
    }

     void initialize()
     {
        displayBox.GetComponent<Text> ().text = "";
        passBox.GetComponent<Text>().text = "ready?";
     }

     void startEvent()
     {
        enabled = true;
     }

    void OnEnabled()
     {
        difficulty = _difficulty;
        difficultyStep = _difficultyStep;
     }

    void Start()
    {
        Coroutine endClock = StartCoroutine(totalCountDown());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (waitingForKey == 0)
        {
            QTEGen = UnityEngine.Random.Range(1,4);
            countingDown = 1;
            if(countDownCoroutine != null) StopCoroutine(countDownCoroutine);
            if(keyPressingCoroutine != null) StopCoroutine(keyPressingCoroutine);
            countDownCoroutine = StartCoroutine(countDown());
            waitingForKey = 1;
        }
        
        switch(QTEGen)
        {
            case 1: 
            {
                displayBox.GetComponent<Text> ().text = "[E]";
                break;
            }
            case 2:
            {
                displayBox.GetComponent<Text> ().text = "[R]";
                break;
            }
            case 3:            
            {
                displayBox.GetComponent<Text> ().text = "[T]";
                break;
            }
            default:break;
        }

        if (Input.GetButtonDown("EKey") || Input.GetButtonDown("RKey") || Input.GetButtonDown("TKey"))
        {//this could all go in the switch but it was cleaner out here, 
            if      (QTEGen == 1 && Input.GetButtonDown("EKey")){correctKey = 1; QTEGen = 20; keyPressingCoroutine = StartCoroutine(KeyPressing());}
            else if (QTEGen == 2 && Input.GetButtonDown("RKey")){correctKey = 1; QTEGen = 20; keyPressingCoroutine = StartCoroutine(KeyPressing());}
            else if (QTEGen == 3 && Input.GetButtonDown("TKey")){correctKey = 1; QTEGen = 20; keyPressingCoroutine = StartCoroutine(KeyPressing());}
            else if (QTEGen <= 3) {correctKey = 2; QTEGen = 20; keyPressingCoroutine = StartCoroutine(KeyPressing());}
        }



    }

    IEnumerator KeyPressing()
    {
        if(countDownCoroutine != null) StopCoroutine(countDownCoroutine);
        QTEGen = 20;//arbitrarilly high
        if (correctKey == 1)
        {
            countingDown = 2;
            displayBox.GetComponent<Text> ().text = "";
            passBox.GetComponent<Text>().text = "Good!";
            difficulty -= difficultyStep;
            correctKey=0;
            skill.damage += skill.bonus;
            if(skill.damage >= skill.maxDamage)
            {
                //TODO end the skill
                //TODO play final animations
                endQTE();
            }
        }
        if (correctKey == 2)
        {
            countingDown = 2;
            displayBox.GetComponent<Text> ().text = "";
            passBox.GetComponent<Text>().text = "Wrong!";
            correctKey=0;
        }

        yield return new WaitForSeconds(1);
        passBox.GetComponent<Text>().text = "";
        displayBox.GetComponent<Text>().text="";
        yield return new WaitForSeconds(0.5f);
        correctKey=0;
        waitingForKey = 0;
        countingDown=1;
    }

    IEnumerator countDown(){
        float elapsed = 0f;
        while (elapsed < difficulty)
        {
            elapsed += Time.deltaTime;
            timer.fillAmount = Mathf.Lerp(1, 0, elapsed / difficulty);
            yield return null;
        }
        if (countingDown == 1)
        {
            if(keyPressingCoroutine != null) StopCoroutine(keyPressingCoroutine);
            QTEGen = 20;
            countingDown = 2;
            passBox.GetComponent<Text>().text = "Too Slow!";
            yield return new WaitForSeconds(.5f);
            correctKey=0;
            // passBox.GetComponent<Text>().text = "";
            displayBox.GetComponent<Text>().text="";
            yield return new WaitForSeconds(0.1f);
            waitingForKey = 0;
            countingDown=1;
        }
    }

    IEnumerator totalCountDown()
    {
        float elapsedTot = 0f;
        while (elapsedTot < totalTime)
        {
            elapsedTot += Time.deltaTime;
            yield return null;
        }
        endQTE();
    }

    protected void endQTE()
    {
        displayBox.GetComponent<Text> ().text = skill.damage.ToString();
        passBox.GetComponent<Text>().text = "Good Job!";
        enabled = false;
        waitingForKey = 0;
        StopAllCoroutines();
        onQTEComplete();
    }

}
