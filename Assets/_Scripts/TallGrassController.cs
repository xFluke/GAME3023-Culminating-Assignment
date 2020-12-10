using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TallGrassController : MonoBehaviour
{
    [SerializeField]
    private int encounterChance = 10;

    public UnityEvent onEnemyEncountered;

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude < 0.1f) {
            // Not Moving
            return;
        }

        if (Random.Range(0, 999) < encounterChance) {
            Debug.Log("ENCOUNTERED AN ENEMY");
            onEnemyEncountered.Invoke();
        }
    }
}
