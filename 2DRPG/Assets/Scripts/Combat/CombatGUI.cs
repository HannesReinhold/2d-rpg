using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatGUI : MonoBehaviour
{
    public GameObject guiCanvas;

    public void Open()
    {
        guiCanvas.SetActive(true);
    }

    public void Close()
    {
        guiCanvas?.SetActive(false);
    }
}
