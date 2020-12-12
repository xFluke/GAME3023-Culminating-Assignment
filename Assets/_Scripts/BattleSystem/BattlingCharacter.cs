using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlingCharacter : MonoBehaviour
{
    [SerializeField]
    int health;
    public int Health {
        get { return health; }
        set { health = value; }
    }

    [SerializeField]
    Ability[] abilities;

    public void SetAbilities(Ability[] _abilities) {
        abilities = _abilities;
    }

    public Ability GetAbilityAtIndex(int index) {
        return abilities[index];
    }
}
