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

    private new string name;
    public string Name {
        get { return name; }
        set { name = value; }
    }


    private int maxHealth;
    public int MaxHealth {
        get { return maxHealth; }
        set {
            maxHealth = value;
              health = value; 
        }
    }

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

    public Ability GetRandomAbility() {
        return abilities[Random.Range(0, abilities.Length - 1)];
    }
}
