using System.Collections;
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

    public UnityEvent<int, BattlingCharacter> onDoingDamage;

    public override void Cast(BattlingCharacter target) {
        if (AttemptCast()) {
            onDoingDamage.Invoke(damage, target);
        }
        else {
            onAbilityCastFail.Invoke();
        }

    }
}
