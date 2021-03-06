﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DamageAbility", menuName = "ScriptableObjects/DamageAbility")]
public class DamageAbility : Ability
{
    [SerializeField]
    int damage;
    public int Damage {
        get { return damage; }
    }

    [TextArea]
    [SerializeField]
    string failText;

    public UnityEvent<int, BattlingCharacter, string> onDoingDamage;

    public override void Cast(BattlingCharacter target) {
        if (AttemptCast()) {
            onDoingDamage.Invoke(damage, target, Name);
        }
        else {
            Debug.Log("MISSED");
            onAbilityCastFail.Invoke(failText);
        }
        
    }
}
