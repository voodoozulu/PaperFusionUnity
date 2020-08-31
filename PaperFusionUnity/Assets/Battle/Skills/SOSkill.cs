using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/skillContainer")]

public class SOSkill :ScriptableObject
{
    public Sprite sprite;
    public Skill skill;
    public string skillname;

    
}
