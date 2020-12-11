using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private GameObject player;
    [SerializeField]
    private GameObject enemy;
    
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
        enemy.GetComponent<SpriteRenderer>().sprite = e.Sprite;

        // Setup Player
        player.GetComponent<BattlingCharacter>().SetAbilities(playerAbilities);

       
    }
}
