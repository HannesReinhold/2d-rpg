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
        
        GUIManager guiManager = FindAnyObjectByType<GUIManager>();
        guiManager.SetDialogGUI(true);
        guiManager.SetDialogText(npcController.GetNextMessage());
        guiManager.SetDialogGUIPosition(npcController.transform.position);
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


}
