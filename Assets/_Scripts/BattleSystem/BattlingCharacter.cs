using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Player = 0,
    Enemy = 1
}
public class BattlingCharacter : MonoBehaviour
{   
    public CharacterType characterType;

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
