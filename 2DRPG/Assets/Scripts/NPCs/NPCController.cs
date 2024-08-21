using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{

    public Animator animator;

    public Vector2 startDirection = new Vector2(0,-1);
    public List<string> dialogTexts;
    private int dialogID = 0;

    void Start()
    {
        animator.Play(0, -1, Random.value);
        ResetDirection();
    }


    void Update()
    {
        
    }

    public void EngageCombat()
    {
        GameManager.Instance.combatManager.StartCombat();
    }

    public string GetNextMessage()
    {
        string text = dialogTexts[dialogID];
        dialogID++;
        dialogID %= dialogTexts.Count;
        return text;
    }

    public void SetDirection(Vector2 dir)
    {
        animator.SetFloat("HorizontalDir", dir.x);
        animator.SetFloat("VerticalDir", dir.y);
    }

    public void ResetDirection()
    {
        animator.SetFloat("HorizontalDir", startDirection.x);
        animator.SetFloat("VerticalDir", startDirection.y);
    }
}
