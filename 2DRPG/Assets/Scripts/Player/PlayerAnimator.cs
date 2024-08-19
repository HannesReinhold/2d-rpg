using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;


    public void SetState(Vector2 dir, float speed)
    {
        animator.SetFloat("HorizontalDir", dir.x);
        animator.SetFloat("VerticalDir", dir.y);
        animator.SetFloat("Speed", speed);
    }
}
