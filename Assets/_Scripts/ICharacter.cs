using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ICharacter : MonoBehaviour
{
    public int healthPoints = 10;

    [SerializeField]
    protected Ability[] abilities = new Ability[4];

    public UnityEvent<ICharacter, int> OnDamageTaken;
    public UnityEvent<ICharacter, Ability> OnAbilityUsed;



    public void UseAbility(int id)
    {
        OnAbilityUsed.Invoke(this, abilities[id]);
    }

    public virtual void TakeTurn()
    {

    }

    private void OnDestroy()
    {
        OnAbilityUsed.RemoveAllListeners();
        OnDamageTaken.RemoveAllListeners();
    }

    public void TakeDamage(int damageTaken)
    {
        healthPoints -= damageTaken;
        
        OnDamageTaken.Invoke(this, damageTaken);
    }
}
