using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState{START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{

    public BattleState state;
    public GameObject playerBattleStation;
    public GameObject enemyBattleStation;
    public Transform enemyPrefab;
    public Transform playerPrefab;
    void Start()
    {
         state = BattleState.START;
        SetupBattle(); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetupBattle() // pass enemy list
    {
        // create children in enemyBattleStation for every enemy in enemy list. (this functionality doesn't suppport bringing in extra enemies)
        //Instantiate(enemy, childtransform)
        //intantiate enemies in transform places?
        
        for (int i = 0; i < 6; i++)
        {
            Instantiate(enemyPrefab, enemyBattleStation.transform.right * 2 * i, Quaternion.identity,enemyBattleStation.transform);
        }
        for (int i = 0; i < 2; i++)
        {
            Instantiate(playerPrefab,  playerBattleStation.transform.right * -2 * i, Quaternion.identity,playerBattleStation.transform);
        }
    }
}
