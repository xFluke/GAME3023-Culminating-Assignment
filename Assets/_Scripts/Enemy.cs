using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy", order = 1)]
public class Enemy : ScriptableObject
{
    [SerializeField]
    private new string name;
    public string Name {
        get { return name; }
    }

    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite {
        get { return sprite; }
    }

    [SerializeField]
    private int maxHP;
    public int MaxHP {
        get { return maxHP; }
    }

    [SerializeField]
    Ability[] abilities;
    
    public Ability[] Abilities {
        get { return abilities; }
    }
}
