using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionMenu : MonoBehaviour
{
    public ToggleGroup interactionOptions;
    public NpcInteractable npc;

    private int selectedOption = 0;

    private void OnEnable()
    {
        interactionOptions.transform.GetChild(0).GetComponent<Toggle>().Select();
        interactionOptions.transform.GetChild(0).GetComponent<Toggle>().isOn = true;
        selectedOption = 0;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return)) npc.Interact((NpcInteractionType)selectedOption);
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            selectedOption = Mathf.Max(0, selectedOption - 1);
            interactionOptions.transform.GetChild(selectedOption).GetComponent<Toggle>().isOn = true;
            Debug.Log("Select Up");
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            selectedOption = Mathf.Min(2, selectedOption + 1);
            interactionOptions.transform.GetChild(selectedOption).GetComponent<Toggle>().isOn = true;
            Debug.Log("Select Down");
        }
    }

    public void SetSelection(int i)
    {
        selectedOption = i;
    }
}
