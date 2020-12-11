using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleButtonHandler : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField]
    GameObject[] buttons;

    void Start() {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.onBattleSceneLoaded.AddListener(OnSceneLoaded);
    }

    void OnSceneLoaded(Enemy e, Ability[] playerAbilities) {
        for (int i = 0; i < playerAbilities.Length; i++) {
            
            if (playerAbilities[i] != null) {
                Debug.Log(playerAbilities[i].Name);

                buttons[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = playerAbilities[i].Name;

                Debug.Log("Changing Name of " + buttons[i].name + "to " + playerAbilities[i]);
            }
            else {
                buttons[i].gameObject.SetActive(false);
            }   
        }
    }
}
