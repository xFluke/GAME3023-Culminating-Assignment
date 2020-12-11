using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    [SerializeField]
    private new string name;
    public string Name {
        get { return name; }
    }

    [TextArea]
    [SerializeField]
    string description;
    public string Description {
        get { return description; }
    }

    [SerializeField]
    float successChance = 0.0f;
    public float SuccessChance {
        get { return successChance; }
    }
    
}
