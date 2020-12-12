using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystemUI : MonoBehaviour
{
    [SerializeField]
    Scrollbar playerHealthBar;

    [SerializeField]
    Scrollbar enemyHealthBar;

    [SerializeField]
    int animateSpeed;

    
    void Start()
    {
        FindObjectOfType<BattleSystem>().onCharacterHealthUpdate.AddListener(AnimateHPBar);
    }

    IEnumerator animateHPRoutine = null;
    public void AnimateHPBar(BattlingCharacter target, int damage)
    {
        if (animateHPRoutine != null)
        {
            StopCoroutine(animateHPRoutine);
        }
        animateHPRoutine = AnimateHPBarRoutine(target, damage);
        StartCoroutine(animateHPRoutine);
    }

    IEnumerator AnimateHPBarRoutine(BattlingCharacter target, int damage)
    {
        int startingHealth = target.Health;
        int endHealth = target.Health - damage;

        Scrollbar targetScrollbar;
        if (target.characterType == CharacterType.Player)
        {
            targetScrollbar = playerHealthBar;
        }
        else
        {
            targetScrollbar = enemyHealthBar;
        }

        for (int currentHealth = startingHealth; currentHealth > endHealth; currentHealth--) 
        {
            targetScrollbar.size = (float)currentHealth / target.maxHealth;
            Debug.Log((float)currentHealth / target.maxHealth);
            yield return new WaitForSeconds(1 / animateSpeed);
        }

        animateHPRoutine = null;
    }
}
