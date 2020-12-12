using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

enum BattlePhase
{
    Player = 0,
    Enemy = 1,
    Count = 2
}

public class BattleSystem : MonoBehaviour
{
    [SerializeField]
    BattlePhase battlePhase;

    private GameManager gameManager;

    [SerializeField]
    private BattlingCharacter player;
    [SerializeField]
    private BattlingCharacter enemy;

    public UnityEvent<BattlingCharacter> onBattlingCharacterDeath;
    public UnityEvent<BattlingCharacter, int> onCharacterHealthUpdate;
    public UnityEvent<string, string> onAbilityDescriptionUpdate;
    public UnityEvent hideUI;
    public UnityEvent showUI;
    public UnityEvent<string> updateBattleText;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.onBattleSceneLoaded.AddListener(OnSceneLoaded);
    }

    public void AdvanceTurns()
    {
        battlePhase++;
        if (battlePhase >= BattlePhase.Count)
        {
            battlePhase = BattlePhase.Player;
        }
    }

   void OnSceneLoaded(Enemy e, Ability[] playerAbilities) {
        // Setup Enemy
        enemy.gameObject.GetComponent<SpriteRenderer>().sprite = e.Sprite;

        // Setup Player
        player.SetAbilities(playerAbilities);

        // Listen for button presses
        BattleButtonHandler battleButtonHandler = FindObjectOfType<BattleButtonHandler>();
        battleButtonHandler.onAbilityButtonPressed.AddListener(OnAbilityButtonPressed);
        battleButtonHandler.onAbilityButtonHovered.AddListener(OnAbilityButtonHovered);
        battleButtonHandler.onAbilityButtonExit.AddListener(OnAbilityButtonExit);

        // Listen for ability casts
        ListenForAbilityCasts(playerAbilities);

        FindObjectOfType<BattleSystemUI>().onHPBarAnimationCompleted.AddListener(OnHPBarAnimationCompleted);
    }

    private void OnHPBarAnimationCompleted() {
        showUI.Invoke();
    }

    private void OnAbilityButtonExit(int index) {
        onAbilityDescriptionUpdate.Invoke("", "");
    }

    private void OnAbilityButtonHovered(int index) {
        onAbilityDescriptionUpdate.Invoke(player.GetAbilityAtIndex(index).Description, player.GetAbilityAtIndex(index).SuccessChance.ToString() + "%");
    }

    private void ListenForAbilityCasts(Ability[] playerAbilities) {
        foreach (Ability ability in playerAbilities) {
            if (ability is DamageAbility) {
                DamageAbility temp = (DamageAbility)ability;
                temp.onDoingDamage.AddListener(OnDamageAbilityCast);
            }
            else if (ability is MiscAbility) {
                MiscAbility temp = (MiscAbility)ability;
                temp.onMiscAbilityCastSuccess.AddListener(OnMiscAbilityCastSuccess);
            }
            ability.onAbilityCastFail.AddListener(OnAbilityCastFail);
        }
    }

    void OnAbilityButtonPressed(int index) {
        Debug.Log("CASTING ABILITY: " + player.GetAbilityAtIndex(index).Name);
        player.GetAbilityAtIndex(index).Cast(enemy);
    }

    void OnDamageAbilityCast(int dmg, BattlingCharacter target, string abiityName) {
        // Hide UI
        hideUI.Invoke();

        // Animate

        // Do Damage
        target.Health -= dmg;
        Debug.Log("DID " + dmg + " damage to " + target.name);

        // Update Health Bars
        onCharacterHealthUpdate.Invoke(target, dmg);

        // Display Battle Text
        updateBattleText.Invoke(target.name + " took " + dmg + " damage");

        // Check for Death
        if (target.Health <= 0) {
            onBattlingCharacterDeath.Invoke(target);
            updateBattleText.Invoke(target.name + " died");

            // end battle scene
        }

        // Advance Turn
    }

    void OnMiscAbilityCastSuccess(string successText) {
        Debug.Log(successText);
    }

    void OnAbilityCastFail(string failText) {
        // Hide UI
        hideUI.Invoke();
        
        // Update Battle Text
        updateBattleText.Invoke(failText);

        // Delay couple seconds and then show UI
        StartCoroutine(DelayShowingUI());

        // Advance Turn
    }

    IEnumerator DelayShowingUI() {
        yield return new WaitForSeconds(2);

        showUI.Invoke();
    }
}
