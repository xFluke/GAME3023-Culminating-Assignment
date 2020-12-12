using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlingCharacter : MonoBehaviour
{   
    public int maxHealth;

    [SerializeField]
    int health;
    public int Health {
        get { return health; }
        set { health = value; }
    }

    [SerializeField]
    Ability[] abilities;

    void Start()
    {
        health = maxHealth;
    }


    public void SetAbilities(Ability[] _abilities) {
        abilities = _abilities;
    }

    public Ability GetAbilityAtIndex(int index) {
        return abilities[index];
    }
}
