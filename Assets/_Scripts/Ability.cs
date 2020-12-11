using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    [SerializeField]
    string  abilityName = "Ability";

    [TextArea]
    [SerializeField]
    string description = "";    

    public int damage = 0;

    [SerializeField]
    float hitChance = 0.0f;



    
}
