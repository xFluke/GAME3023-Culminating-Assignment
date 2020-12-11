using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private EnemyTable enemyTable;

    public UnityEvent<Enemy> onBattleSceneLoaded;

    void Start() {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadBattleScene() {
        SceneManager.LoadScene("Battle");
        StartCoroutine(FireBattleSceneLoadedEvent());
    }

    IEnumerator FireBattleSceneLoadedEvent() {
        yield return new WaitForSeconds(0.1f);

        onBattleSceneLoaded.Invoke(enemyTable.GetRandomEnemy());
    }
}
