using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    LEFT,
    RIGHT,
    UP,
    DOWN

}

public class CharacterWalkAnimationController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    Direction walkDirection = Direction.DOWN;

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (velocity.magnitude > float.Epsilon)
        {
            if(Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
            {
                if (velocity.x < 0)
                {
                    walkDirection = Direction.LEFT;
                }
                else
                {
                    walkDirection = Direction.RIGHT;
                }
            }
            else
            {
                if (velocity.y < 0)
                {
                    walkDirection = Direction.DOWN;
                }
                else
                {
                    walkDirection = Direction.UP;
                }
            }
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
        animator.SetInteger("WalkDirection", (int)walkDirection);
    }
}
