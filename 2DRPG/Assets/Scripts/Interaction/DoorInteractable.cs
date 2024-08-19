using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : Interactable
{
    public int doorID;
    public string targetSceneName;
    public int targetDoorID;

    public Transform spawn;

    private void Awake()
    {
        interactionType = InteractableType.Door;
    }

    public override void Interact(Transform playerTransform)
    {
        Debug.Log("Enter House "+ doorID + ", "+targetSceneName);
        GameManager.Instance.EnterLevel(targetSceneName, targetDoorID);
    }

    public override void ResetInteraction()
    {
        Debug.Log("Leaving Door" + doorID);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
