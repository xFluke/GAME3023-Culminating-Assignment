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

        
        /*if (animateAttackRoutine != null)
        {
            StopCoroutine(animateAttackRoutine);
        }
        animateAttackRoutine = AnimateAttackingRoutine(target);
        StartCoroutine(animateAttackRoutine);*/
    }

    /*IEnumerator AnimateAttackingRoutine(BattlingCharacter target)
    {
        int xDirection = 1;
        int yDirection = 1;
        

        for (int currentHealth = 0; currentHealth < 20; currentHealth++) 
        {
            target.transform.position = new Vector3(
                target.transform.position.x + 0.1f * xDirection, 
                target.transform.position.y + 0.1f * yDirection, 
                target.transform.position.z);
            yield return new WaitForSeconds(1 / animateSpeed);
        }

        animateAttackRoutine = null;
    }*/
}
