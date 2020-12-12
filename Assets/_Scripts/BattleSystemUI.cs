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
    BattlingCharacter player;
    [SerializeField]
    BattlingCharacter enemy;

    IEnumerator animateHPRoutine = null;
    public void AnimateHPBar(int damage)
    {
        if (animateHPRoutine != null)
        {
            StopCoroutine(animateHPRoutine);
        }
        animateHPRoutine = AnimateTextRoutine(damage);
        StartCoroutine(animateHPRoutine);
    }

    IEnumerator AnimateTextRoutine(int damage)
    {
        int startingHealth = player.Health;
        int endHealth = player.Health - damage;

        for (int currentHealth = startingHealth; currentHealth > endHealth; currentHealth--) 
        {
            playerHealthBar.size = currentHealth / player.maxHealth;
            yield return new WaitForSeconds(1 / 60);
        }

        animateHPRoutine = null;
    }
}
