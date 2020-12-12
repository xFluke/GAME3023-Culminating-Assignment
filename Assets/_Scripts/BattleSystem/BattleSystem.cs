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

        // Listen for ability casts
        ListenForAbilityCasts(playerAbilities);
    }

    private void ListenForAbilityCasts(Ability[] playerAbilities) {
        foreach (Ability ability in playerAbilities) {
            if (ability is DamageAbility) {
                DamageAbility temp = (DamageAbility)ability;
                temp.onDoingDamage.AddListener(OnDoingDamage);
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

    void OnDoingDamage(int dmg, BattlingCharacter target, string abiityName) {
        target.Health -= dmg;

        Debug.Log("DID " + dmg + " damage to " + target.name);
        onCharacterHealthUpdate.Invoke(enemy, dmg);

        if (target.Health <= 0) {
            onBattlingCharacterDeath.Invoke(target);
            Debug.Log(target + " died");
        }
    }

    void OnMiscAbilityCastSuccess(string successText) {
        Debug.Log(successText);
    }

    void OnAbilityCastFail(string failText) {
        Debug.Log(failText);
    }
}
