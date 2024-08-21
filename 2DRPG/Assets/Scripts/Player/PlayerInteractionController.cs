using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerInteractionController : MonoBehaviour
{

    public PlayerMovement movementController;

    private List<Interactable> nearInteractables = new List<Interactable>();
    private bool insideInteractable = false;

    public Tilemap interactionTileMap;

    private GUIManager guiManager;

    private Vector3Int lastMapPos;
    
    void Start()
    {
        guiManager = FindAnyObjectByType<GUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }

        GroundInteraction();
    }

    private void GroundInteraction()
    {
        Vector3 playerPos = movementController.transform.position;
        Vector3Int mapPos = interactionTileMap.WorldToCell(playerPos);

        if(lastMapPos != mapPos)
        {
            lastMapPos = mapPos;
            if(insideInteractable && Random.Range(1,100)>95)
            {
                GameManager.Instance.guiManager.OpenHint(playerPos + new Vector3(0,1.25f,0));
                GameManager.Instance.combatManager.StartCombat();
            }
        }
        
    }

    private void TryInteract()
    {
        Debug.Log("Try Interact");
        if (nearInteractables.Count == 0) return;
        Interactable  interactable = nearInteractables[0];
        interactable.Interact(movementController.transform);
        if (interactable.RemoveAfterInteract) nearInteractables.Remove(interactable);
    }

    private bool TryInteractMap()
    {
        Debug.Log("Try Interact Map");

        Vector3 dir = new Vector3(movementController.currentDirection.x, movementController.currentDirection.y, 0);
        Vector3Int mapPos = interactionTileMap.WorldToCell(movementController.transform.position+dir * 0.5f);

        TileBase tile = interactionTileMap.GetTile(mapPos);
        if (tile == null) return false;

        ShowInfo(interactionTileMap.CellToWorld(mapPos)+new Vector3(0.5f,1,0), tile.name);

        return true;
    }

    private void ShowInfo(Vector3 pos, string text)
    {
        guiManager.SetDialogGUI(true);
        guiManager.SetDialogGUIPosition(pos);
        guiManager.SetDialogText(text);

        Invoke("CloseInfo",3);
    }

    private void CloseInfo()
    {
        guiManager.SetDialogText("");
        guiManager.SetDialogGUI(false);
    }

    private bool TryInteractNPC()
    {
        Debug.Log("Try Interact NPC");
        if (nearInteractables.Count == 0) return false;
        NPCController npc = nearInteractables[0].GetComponentInParent<NPCController>();
        if (npc == null) return false;
        if (!DoesPlayerFaceInteractable(npc.transform.position)) return false;

        FacePlayer(npc);

        guiManager.SetDialogGUI(true);
        guiManager.SetDialogText(npc.GetNextMessage());
        guiManager.SetDialogGUIPosition(npc.transform.position);

        return true;
    }

     private bool DoesPlayerFaceInteractable(Vector2 pos)
    {
        Vector2 playerPos = movementController.transform.position;
        Vector2 dir = (playerPos - pos).normalized;
        return Vector2.Dot(movementController.currentDirection.normalized, dir) < 0.5f;
    }

    private void FacePlayer(NPCController npc)
    {
        Vector2 playerPos = movementController.transform.position;
        Vector2 npcPos = npc.transform.position;
        Vector2 dir = (playerPos - npcPos).normalized;
        npc.SetDirection(dir);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactable interactable = collision.GetComponentInParent<Interactable>();
        if(interactable!=null) nearInteractables.Add(interactable);

        if (collision.GetComponent<TilemapCollider2D>())
            insideInteractable = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Interactable interactable = collision.GetComponentInParent<Interactable>();
        if (interactable != null)
        {
            nearInteractables.Remove(interactable);
            interactable.ResetInteraction();
        }

        insideInteractable=false;


        guiManager.SetDialogGUI(false);
        guiManager.SetDialogText("");
    }
}
