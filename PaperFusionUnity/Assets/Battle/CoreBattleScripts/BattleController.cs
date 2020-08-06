using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState{START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class BattleController : MonoBehaviour
{
    public BattleState state;
    public GameObject playerBattleStation;
    public GameObject enemyBattleStation;
    public GameObject enemyPrefab;
    public GameObject cinnaprefab;
    private GameObject cinna;
    public GameObject fuseprefab;
    private GameObject fuse;
    private List<GameObject> enemyList = new List<GameObject>();

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
        //Instantiate(enemy, childtransform)
        //intantiate enemies in transform places?
        int i = 0;
        foreach (GameObject enemy in myEnemies)
        {

            enemyList.Add(Instantiate(enemy, enemyBattleStation.transform));
            enemyList[enemyList.Count - 1].transform.Translate(1*i,0,0);
            i++;
        }
        cinna = Instantiate(cinnaprefab,playerBattleStation.transform);
        cinna.transform.Translate(0,0,0);
        cinna.name = "Cinna";
        fuse = Instantiate(fuseprefab,playerBattleStation.transform);
        fuse.transform.Translate(-1,0,0);
        fuse.name = "Fuse";

        // for (int i = 0; i < 2; i++)
        // {
        //     Instantiate(playerPrefab,  playerBattleStation.transform.right * -2 * i, Quaternion.identity,playerBattleStation.transform);
        // }
        
    }
}

