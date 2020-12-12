using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameManager() { }
    private static GameManager instance;
    public static GameManager Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }

        private set { }
    }

    [SerializeField]
    private EnemyTable enemyTable;

    [SerializeField]
    private Ability[] playerAbilities;

    public UnityEvent<Enemy, Ability[]> onBattleSceneLoaded;

    void Start() {
        GameManager[] gameManagers = FindObjectsOfType<GameManager>();
        foreach (GameManager mgr in gameManagers) {
            if (mgr != Instance) {
                Destroy(mgr.gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);

        FindObjectOfType<TallGrassController>().onEnemyEncountered.AddListener(LoadBattleScene);
    }

    public void LoadBattleScene() {
        SceneManager.LoadScene("Battle");
        StartCoroutine(FireBattleSceneLoadedEvent());

    }

    IEnumerator FireBattleSceneLoadedEvent() {
        yield return new WaitForSeconds(0.1f);

        onBattleSceneLoaded.Invoke(enemyTable.GetRandomEnemy(), playerAbilities);

        FindObjectOfType<BattleSystem>().onBattleEnd.AddListener(OnBattleEnd);

    }

    void OnBattleEnd() {
        SceneManager.LoadScene("Overworld");
    }
}
