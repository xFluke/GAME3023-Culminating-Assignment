using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleSystemEnemyAI : MonoBehaviour
{
    public UnityEvent onEnemyTurnEnd;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<BattleSystem>().onEnemyTurnStart.AddListener(OnEnemyTurn);
    }

    void OnEnemyTurn(BattlingCharacter enemy, BattlingCharacter player) {
        Debug.Log("started enemy turn");
        enemy.GetRandomAbility().Cast(player);
    }
}
