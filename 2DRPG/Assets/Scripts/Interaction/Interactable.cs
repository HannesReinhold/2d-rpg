using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool RemoveAfterInteract;

    public InteractableType interactionType;

    public abstract void Interact(Transform playerTransform);
    public abstract void ResetInteraction();
}

[System.Serializable]
public enum InteractableType
{
    Npc,
    Door,
    InfoText
}