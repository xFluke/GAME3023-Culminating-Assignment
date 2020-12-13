using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BattleSystemUI : MonoBehaviour
{
    [SerializeField]
    Slider playerHealthBar;

    [SerializeField]
    Slider enemyHealthBar;

    [SerializeField]
    float animateSpeed;

    [SerializeField]
    TMPro.TextMeshProUGUI abilityDescription;

    [SerializeField]
    TMPro.TextMeshProUGUI successChanceLabel;

    [SerializeField]
    TMPro.TextMeshProUGUI battleText;

    public UnityEvent onHPBarAnimationCompleted;

    void Start()
    {
        FindObjectOfType<BattleSystem>().onCharacterHealthUpdate.AddListener(AnimateHPBar);
        FindObjectOfType<BattleSystem>().onAbilityDescriptionUpdate.AddListener(UpdateAbilityDescription);
        FindObjectOfType<BattleSystem>().updateBattleText.AddListener(UpdateBattleText);
        FindObjectOfType<GameManager>().onBattleSceneLoaded.AddListener(OnSceneLoaded);
    }

    private void OnSceneLoaded(Enemy e, Ability[] abilities) {
        enemyHealthBar.maxValue = e.MaxHP;
        enemyHealthBar.value = e.MaxHP;
        playerHealthBar.maxValue = 300;
        playerHealthBar.value = 300;
    }

    private void UpdateBattleText(string text) {
        battleText.text += text + "\n";
    }

    private void UpdateAbilityDescription(string description, string successChance) {
        abilityDescription.text = description;
        successChanceLabel.text = successChance;
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
        Debug.Log("ANIMATING HP BAR");

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
        onHPBarAnimationCompleted.Invoke();
    }
}
