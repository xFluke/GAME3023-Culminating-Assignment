using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Table", menuName = "ScriptableObjects/EnemyTable", order = 2)]
public class EnemyTable : ScriptableObject
{
    [SerializeField]
    private Enemy[] enemies;

    public int GetSize() {
        return enemies.Length;
    }

    public Enemy GetRandomEnemy() {
        return enemies[Random.Range(0, enemies.Length - 1)];
    }
}
