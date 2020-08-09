using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState{START, PLAYERTURN, ENEMYTURN, WON, LOST}
public enum TargetRequest{PLAYERLIST, ENEMYLIST, ALL}
public class BattleController : MonoBehaviour
{
    public BattleState state;
    public GameObject playerBattleStation;
    public GameObject enemyBattleStation;

    public GameObject enemyPrefab;

    public GameObject cinnaprefab;
    private GameObject cinna;
    private Player cinnaBattler;

    public GameObject fuseprefab;
    private GameObject fuse;
    private Player fuseBattler;
    
    private List<GameObject> playerContainerList = new List<GameObject>();
    private List<GameObject> enemyContainerList = new List<GameObject>();

    private List<GameObject> myEnemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    { 
        state = BattleState.START;
        myEnemies.Add(enemyPrefab);
        myEnemies.Add(enemyPrefab);
        myEnemies.Add(enemyPrefab);
        setupBattle(myEnemies); 
    }

    // Update is called once per frame
    void Update()
    {
        
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
            enemyContainerList[enemyContainerList.Count - 1].GetComponent<Enemy>().initialize(this);
            i++;
        }

        //Instantiate Cinna and "fuse"
        cinna = Instantiate(cinnaprefab,playerBattleStation.transform);
        cinna.transform.Translate(0,0,0);
        cinna.name = "Cinna";
        playerContainerList.Add(cinna);
        cinnaBattler = cinna.GetComponent<Player>();
        cinnaBattler.initialize(this);

        fuse = Instantiate(fuseprefab,playerBattleStation.transform);
        fuse.transform.Translate(-1,0,0);
        fuse.name = "Fuse";
        playerContainerList.Add(fuse);
        fuseBattler = fuse.GetComponent<Player>();
        fuseBattler.initialize(this);

        state = BattleState.PLAYERTURN;
        // mainPhase();
    }

    private void mainPhase()
    {
        while(state != BattleState.WON && state != BattleState.LOST)
        {
            playerTurn();
            enemyTurn();
        }
    }

    private void playerTurn()
    {
        state = BattleState.PLAYERTURN;
        cinnaBattler.playTurn();
    }

    private void enemyTurn()
    {
        state = BattleState.ENEMYTURN;
        foreach (GameObject enemy in enemyContainerList)
        {
            enemy.GetComponent<Enemy>().playTurn();
        }
    }

    public List<GameObject> getTargets(TargetRequest request)
    {
        switch(request)
        {
            case TargetRequest.PLAYERLIST: return playerContainerList;
            case TargetRequest.ENEMYLIST: return enemyContainerList;
            case TargetRequest.ALL: return playerContainerList.Concat(enemyContainerList).ToList();
            default: return playerContainerList.Concat(enemyContainerList).ToList();
        }
    }

    public void handleHealthDepleted(Battler target) => StartCoroutine(targetKilled(target));
    public IEnumerator targetKilled(Battler target) { yield return new WaitForSeconds(1); Destroy(target.gameObject);}

}

