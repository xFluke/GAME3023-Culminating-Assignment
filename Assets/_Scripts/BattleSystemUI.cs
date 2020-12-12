using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystemUI : MonoBehaviour
{
    [SerializeField]
    Slider playerHealthBar;

    [SerializeField]
    Slider enemyHealthBar;

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
        int startingHealth = target.Health + damage;
        int endHealth = target.Health;

        if (endHealth < 0) {
            endHealth = 0;
        }

        Slider targetSlider;
        if (target.characterType == CharacterType.Player)
        {
            targetSlider = playerHealthBar;
        }
        else
        {
            targetSlider = enemyHealthBar;
        }

        for (int currentHealth = startingHealth; currentHealth >= endHealth; currentHealth--) 
        {
            targetSlider.value = currentHealth;
            yield return new WaitForSeconds(1 / animateSpeed);
        }

        animateHPRoutine = null;
    }
}
