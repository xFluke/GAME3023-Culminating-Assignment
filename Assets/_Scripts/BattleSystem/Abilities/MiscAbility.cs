using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "MiscAbility", menuName = "ScriptableObjects/MiscAbility")]

public class MiscAbility : Ability
{
    public UnityEvent<string> onMiscAbilityCastSuccess;
    //public UnityEvent<string> onMiscAbilityCastFail;

    [TextArea]
    [SerializeField]
    string successText;
    //public string SuccessText {
    //    get { return successText; }
    //}

    [TextArea]
    [SerializeField]
    string failText;
    //public string FailText {
    //    get { return failText; }
    //}

    public override void Cast(BattlingCharacter target) {
        if (AttemptCast()) {
            onMiscAbilityCastSuccess.Invoke(successText);
        }
        else {
            onAbilityCastFail.Invoke(failText);
        }
    }
}
