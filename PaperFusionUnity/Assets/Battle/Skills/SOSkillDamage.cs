using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SkillDamage", menuName = "Skills/Damage")]
public class SOSkillDamage :ScriptableObject
{
    public int damage = 0;
    public int bonus = 1;
}
