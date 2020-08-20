using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    //TODO Scene location setting variable for every scene ie desert, water, forest
    public Scene battleScene;
    public void BattleStart(List<Battler> badGuys, string battleType)
    {
        GlobalControl.Instance.enemiesToFight = badGuys;
        StartCoroutine(LoadAsync(battleType));
    }
    IEnumerator LoadAsync(string battleType)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(battleType);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
