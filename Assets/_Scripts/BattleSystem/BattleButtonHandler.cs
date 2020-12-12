using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleButtonHandler : MonoBehaviour
{
    [SerializeField]
    GameObject[] buttons;

    public UnityEvent<int> onAbilityButtonPressed;

    void Start() {
        GameManager gameManager = FindObjectOfType<GameManager>();
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

    public void ButtonPressed(int index) {
        onAbilityButtonPressed.Invoke(index);
    }
}
