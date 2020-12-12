using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleButtonHandler : MonoBehaviour
{
    [SerializeField]
    GameObject[] buttons;

    public UnityEvent<int> onAbilityButtonPressed;
    public UnityEvent<int> onAbilityButtonHovered;
    public UnityEvent<int> onAbilityButtonExit;


    void Start() {
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.onBattleSceneLoaded.AddListener(OnSceneLoaded);
    }

    void OnSceneLoaded(Enemy e, Ability[] playerAbilities) {
        for (int i = 0; i < playerAbilities.Length; i++) {
            
            if (playerAbilities[i] != null) {
                buttons[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = playerAbilities[i].Name;
            }
            else {
                buttons[i].gameObject.SetActive(false);
            }   
        }
    }

    public void ButtonPressed(int index) {
        onAbilityButtonPressed.Invoke(index);
    }

    public void ButtonHovered(int index) {
        onAbilityButtonHovered.Invoke(index);
    }

    public void ButtonExit(int index) {
        onAbilityButtonExit.Invoke(index);
    }
}
