using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInteractable : Interactable
{
    public NPCController npcController;


    private void Awake()
    {
        interactionType = InteractableType.Npc;
    }

    public override void Interact(Transform playerTransform)
    {
        FacePlayer(playerTransform.position);


        GameManager.Instance.guiManager.OpenInteractionMenu(transform.position + new Vector3(1.5f,0,0));
        GameManager.Instance.guiManager.interactionCanvas.npc = this;
        GameManager.Instance.DisableMovement();
    }

    public override void ResetInteraction()
    {
        npcController.ResetDirection();
    }

    private void FacePlayer(Vector2 playerPos)
    {
        Vector2 npcPos = npcController.transform.position;
        Vector2 dir = (playerPos - npcPos).normalized;
        npcController.SetDirection(dir);
    }

    public override void EndInteraction()
    {
        GameManager.Instance.guiManager.CloseInteractionMenu();
        GameManager.Instance.EnableMovement();
        GameManager.Instance.guiManager.interactionCanvas.npc = null;
    }

    public void Interact(NpcInteractionType type)
    {
        switch (type)
        {
            case NpcInteractionType.Talk:
                Talk();
                break;
            case NpcInteractionType.Fight:
                Fight();
                break;
            default:
                EndInteraction();
                break;
        }
    }

    private void Talk()
    {
        GUIManager guiManager = FindAnyObjectByType<GUIManager>();
        guiManager.SetDialogGUI(true);
        guiManager.SetDialogText(npcController.GetNextMessage());
        guiManager.SetDialogGUIPosition(npcController.transform.position);
    }

    private void Fight()
    {
        npcController.EngageCombat();
        GameManager.Instance.guiManager.CloseInteractionMenu();
    }
}

public enum NpcInteractionType
{
    Talk,
    Fight,
    Leave

}
