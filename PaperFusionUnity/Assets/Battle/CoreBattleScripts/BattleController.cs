using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState{START, PLAYERTURN, ENEMYTURN, WON, LOST}
public enum TargetRequest{PLAYERLIST, ENEMYLIST, ALL}
public class BattleController : MonoBehaviour
{
    /*
    The BattleController directs the interactions between battlers. Battlers should not directly interact with one another to prevent 
    dependancy issues. 
    */
    public BattleState state;
    [SerializeField]
    private bool targeting = false;
    public GameObject playerBattleStation; //Battlestations are just transforms to tell the controller where to put the battlers
    public GameObject enemyBattleStation;

    public GameObject enemyPrefab;

    public GameObject cinnaprefab; //Prefab to be cloned
    private GameObject cinna;      //the clone of the prefab
    private PlayerBattler cinnaBattler;   //The battler object associated with the parent game object (cleaner than gameObject.GetChildType<Battler>() any time you want to reference)

    public GameObject fuseprefab;
    private GameObject jumble;
    private PlayerBattler fuseBattler;
    
    private List<GameObject> playerContainerList = new List<GameObject>(); //list of player characters for targeting
    private List<GameObject> enemyContainerList = new List<GameObject>(); //list of enemy characters for targeting and turn order

    private List<GameObject> myEnemies = new List<GameObject>(); //Temp variable for cloning purposes
    // Start is called before the first frame update
    void Start()
    { 
        //setting up test enemies. in the future setupBattle should be called by the game master-script
        state = BattleState.START;
        myEnemies.Add(enemyPrefab);
        myEnemies.Add(enemyPrefab);
        myEnemies.Add(enemyPrefab);
        setupBattle(myEnemies); 
    }

    // Update is called once per frame
    public Ray ray;
    public RaycastHit hit;
    public RaycastHit oldHit;
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(targeting == true && Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            if(hit.collider.CompareTag("Battler"))
            {
                if(oldHit.collider != null)
                {
                    hideSelected(oldHit);
                }
                oldHit = hit;
                showSelected(hit);
            }
        }
        if(targeting == false && oldHit.collider!= null) hideSelected(oldHit);
    }

    IEnumerator targetingCoroutine()
    {
        while(targeting == true)
        {
            if(oldHit.collider != null){hideSelected(oldHit);}
            if(Physics.Raycast(ray.origin, ray.direction, out hit) && hit.collider.CompareTag("Battler"))
            {
                
            }
            yield return null;
        }
        
    }
    private void showSelected(RaycastHit hitt)//possibly depreciated
    {
        hitt.collider.gameObject.transform.parent.Find("Canvas").Find("targeting").gameObject.SetActive(true);
    }
    private void hideSelected(RaycastHit hitt)//Possibly depreciated
    {
        hitt.collider.gameObject.transform.parent.Find("Canvas").Find("targeting").gameObject.SetActive(false);
    }

    public void setupBattle(List<GameObject> enemies)
    {
        // create children in enemyBattleStation for every enemy in enemy list. (this functionality doesn't suppport bringing in extra enemies)
        //Instantiate enemy
        float i = 0;
        foreach (GameObject enemy in myEnemies)
        {
            
            enemyContainerList.Add(Instantiate(enemy, enemyBattleStation.transform));
            enemyContainerList[enemyContainerList.Count - 1].transform.Translate(i,0,i*0.25f-0.25f);
            enemyContainerList[enemyContainerList.Count - 1].GetComponent<EnemyBattler>().initialize(this);
            i++;
        }

        //Instantiate Cinna and "fuse"
        cinna = Instantiate(cinnaprefab,playerBattleStation.transform); //Adds Cinna as a child of the Battlestation
        cinna.transform.Translate(0,0,0);
        cinna.name = "Cinna";                                           //changes cinna's gameobject name to Cinna for clarity. Unnecessary for code
        playerContainerList.Add(cinna);
        cinnaBattler = cinna.GetComponent<PlayerBattler>();
        cinnaBattler.initialize(this);

        jumble = Instantiate(fuseprefab,playerBattleStation.transform);
        jumble.transform.Translate(-1,0,0);
        jumble.name = "Jumble";
        playerContainerList.Add(jumble);
        fuseBattler = jumble.GetComponent<PlayerBattler>();
        fuseBattler.initialize(this);

        state = BattleState.PLAYERTURN;
        // mainPhase();
    }

    private void mainPhase()
    { //DO NOT CALL THIS, at the moment this causes a crash due to an infinite loop. 
        // while(state != BattleState.WON && state != BattleState.LOST)
        // {
        //     playerTurn();
        //     enemyTurn();
        // }
    }

    private void playerTurn()
    {//not yet implemented
        state = BattleState.PLAYERTURN;
        cinnaBattler.playTurn();
    }

    private void enemyTurn()
    {//not yet implemented
        state = BattleState.ENEMYTURN;
        foreach (GameObject enemy in enemyContainerList)
        {
            enemy.GetComponent<EnemyBattler>().playTurn();
        }
    }

    public List<GameObject> getTargets(TargetRequest request)
    {//returns targets based on skill request. 
        switch(request)
        {
            case TargetRequest.PLAYERLIST: return playerContainerList;
            case TargetRequest.ENEMYLIST: return enemyContainerList;
            case TargetRequest.ALL: return playerContainerList.Concat(enemyContainerList).ToList();
            default: return playerContainerList.Concat(enemyContainerList).ToList();
        }
    }

    public void handleHealthDepleted(Battler target) => StartCoroutine(targetKilled(target));
    public IEnumerator targetKilled(Battler target) { yield return new WaitForSeconds(1); Destroy(target.gameObject);} //destroys game object after it dies. 
    //if we want resurection we should just disable and hide the game object instead. To prevent instantiating out of load-time
}

