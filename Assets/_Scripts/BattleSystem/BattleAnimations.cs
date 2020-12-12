using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleAnimations : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject enemy;

    [SerializeField]
    int animateSpeed;

    [SerializeField]
    Animator playerAnimator;

    [SerializeField]
    Animator enemyAnimator;
    
    void Start()
    {
        FindObjectOfType<BattleSystem>().onCharacterHealthUpdate.AddListener(AnimateAttackingCharacter);
    }

    IEnumerator animateAttackRoutine = null;
    public void AnimateAttackingCharacter(BattlingCharacter target, int damage)
    {
        if (target.characterType == CharacterType.Player)
        {
            enemyAnimator.Play("EnemyAttackAnimation");
        }
        else
        {
            playerAnimator.Play("PlayerAttackAnimation");
        }
    }

}
