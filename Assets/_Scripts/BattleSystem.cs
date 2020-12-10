using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
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

   void OnSceneLoaded(Enemy e) {
        enemy.GetComponent<SpriteRenderer>().sprite = e.Sprite;
    }
}
