using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    public Image foregroundImage;
    [SerializeField]
    private float updateSpeedSeconds = 0.4f;

    void Awake()
    {
        GetComponentInParent<EnemyBattler>().OnHealthChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(int health, int maxHealth)
    {
         StartCoroutine(ChangeToPct(health, maxHealth));
    }

    private IEnumerator ChangeToPct(int health, int maxHealth)
    {
        float preChangedPct = foregroundImage.fillAmount;
        float elapsed = 0f;
        float pct = (float)health/(float)maxHealth;

        while (elapsed < updateSpeedSeconds)
        {//use to slowly change health bar. Change updateSpeedSeconds to make it faster or slower
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangedPct, pct, elapsed / updateSpeedSeconds);
            yield return null;
        }
        foregroundImage.fillAmount = pct;    
    }
    void LateUpdate()
    {
    }
}
