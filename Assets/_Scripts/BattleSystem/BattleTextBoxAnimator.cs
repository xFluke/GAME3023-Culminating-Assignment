using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BattleTextBoxAnimator : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI text;

    [SerializeField]
    [Range(0.001f, 100000.0f)]
    float textSpeedCharactersPerSecond = 60.0f;

    IEnumerator animateTextRoutine = null;

    public void AnimateTextCharacterTurn(ICharacter whoseTurn)
    {
        AnimateText("It is " + whoseTurn.name + "'s turn.");
    }
    public void AnimateText(string message)
    {
        if (animateTextRoutine != null)
        {
            StopCoroutine(animateTextRoutine);
        }
        animateTextRoutine = AnimateTextRoutine(message);
        StartCoroutine(animateTextRoutine);
    }

    IEnumerator AnimateTextRoutine(string message)
    {
        Assert.IsTrue(textSpeedCharactersPerSecond > float.Epsilon);
        string currentMessage = "";

        for (int currentChar = 0; currentChar < message.Length; currentChar++)
        {
            currentMessage += message[currentChar];
            text.text = currentMessage;
            yield return new WaitForSeconds(1 / textSpeedCharactersPerSecond);
        }   

        animateTextRoutine = null;
    }
}
