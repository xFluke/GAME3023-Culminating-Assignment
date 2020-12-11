using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlingCharacter : MonoBehaviour
{
    [SerializeField]
    Ability[] abilities;

    public void SetAbilities(Ability[] _abilities) {
        abilities = _abilities;
    }
}
