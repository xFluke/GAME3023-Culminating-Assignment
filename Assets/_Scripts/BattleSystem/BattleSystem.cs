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

    public UnityEvent onBattleEnd;
    public UnityEvent<BattlingCharacter, int> onCharacterHealthUpdate;
    public UnityEvent<string, string> onAbilityDescriptionUpdate;
    public UnityEvent hideUI;
    public UnityEvent showUI;
    public UnityEvent<string> updateBattleText;
    public UnityEvent<BattlingCharacter, BattlingCharacter> onEnemyTurnStart;

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

        if (battlePhase == BattlePhase.Enemy) {
            Debug.Log("Starting enemy turn");
            onEnemyTurnStart.Invoke(enemy, player);
        }
    }

    void OnSceneLoaded(Enemy e, Ability[] playerAbilities) {
        Debug.Log("Setting up");

        // Setup Enemy
        enemy.gameObject.GetComponent<SpriteRenderer>().sprite = e.Sprite;
        enemy.Name = e.Name;
        enemy.MaxHealth = e.MaxHP;
        
        enemy.SetAbilities(e.Abilities);

        // Setup Player
        player.SetAbilities(playerAbilities);
        player.Name = "Player";
        player.MaxHealth = 300;

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
        if (player.Health <= 0 || enemy.Health <= 0) {
            // Check for Death
            if (enemy.Health <= 0) {

                updateBattleText.Invoke(enemy.Name + " died");
                updateBattleText.Invoke("You Win!");

                StartCoroutine(SwitchScenes());
                return;
            }

            else if (player.Health <= 0) {
                updateBattleText.Invoke("You died");

                StartCoroutine(SwitchScenes());

                return;
            }
        }

        AdvanceTurns();

        if (battlePhase == BattlePhase.Player) {
            showUI.Invoke();
        }
    }
        

    private void OnAbilityButtonExit(int index) {
        onAbilityDescriptionUpdate.Invoke("", "");
    }

    private void OnAbilityButtonHovered(int index) {
        onAbilityDescriptionUpdate.Invoke(player.GetAbilityAtIndex(index).Description, player.GetAbilityAtIndex(index).SuccessChance.ToString() + "%");
    }

    private void ListenForAbilityCasts(Ability[] playerAbilities) {
        foreach (Ability ability in playerAbilities) {
            if (ability != null) {

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
    }

    void OnAbilityButtonPressed(int index) {
        Debug.Log("CASTING ABILITY: " + player.GetAbilityAtIndex(index).Name);
        player.GetAbilityAtIndex(index).Cast(enemy);
    }

    void OnDamageAbilityCast(int dmg, BattlingCharacter target, string abiityName) {
        // Hide UI
        hideUI.Invoke();

        // Do Damage
        target.Health -= dmg;
        Debug.Log("DID " + dmg + " damage to " + target.Name);

        // Update Health Bars and Animate
        onCharacterHealthUpdate.Invoke(target, dmg);

        // Display Battle Text
        updateBattleText.Invoke("Used " + abiityName + " on " + target.Name);
        updateBattleText.Invoke(target.Name + " took " + dmg + " damage" + " from " + abiityName);
        
    }

    void OnMiscAbilityCastSuccess(string successText) {
        // Hide UI
        hideUI.Invoke();

        Debug.Log(successText);
        updateBattleText.Invoke(successText);

        StartCoroutine(SwitchScenes());
        
    }

    void OnAbilityCastFail(string failText) {
        // Hide UI
        hideUI.Invoke();
        
        // Update Battle Text
        updateBattleText.Invoke(failText);

        // Delay couple seconds and then show UI
        StartCoroutine(DelayShowingUI());
    }

    IEnumerator DelayShowingUI() {
        yield return new WaitForSeconds(2);

        showUI.Invoke();

        // Advance Turn
        AdvanceTurns();
    }

    IEnumerator SwitchScenes() {
        yield return new WaitForSeconds(2);

        onBattleEnd.Invoke();

    }
}
