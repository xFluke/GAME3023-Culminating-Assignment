using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    //[TextArea]
    //[SerializeField]
    //string successText;
    //public string SuccessText {
    //    get { return successText; }
    //}


    public UnityEvent<string> onAbilityCastFail;

    protected bool AttemptCast() {
        if (Random.Range(1, 100) < successChance) {
            return true;
        }
        else {
            return false;
        }
    }

    public virtual void Cast(BattlingCharacter target) { }

}
