using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;
using UnityEngine.UI;

public class CombatUiButton : Button
{
    /* the idea of this class is to keep a disabled button around a game object for UI actions in combat. On combat start these buttons should be created
     and have images/text assigned to them as well as On-Click actions enbled. */
    // Start is called before the first frame update
    [Header("Skill Settings")]
    public SOSkill skill;
    

    void Start()
    {
        
        //skill.skill.enabled = true;
    }

    // Update is called once per frame

    
}
