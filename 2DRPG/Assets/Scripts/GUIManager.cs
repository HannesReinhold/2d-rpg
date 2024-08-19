using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour
{
    public Canvas dialogWindow;
    public TextMeshProUGUI dialogText;

    public bool isVisible;

    private void Awake()
    {
        SetDialogGUI(isVisible);
        DontDestroyOnLoad(gameObject);
    }

    public void SetDialogGUI(bool vis)
    {
        dialogWindow.enabled = vis;
    }

    public void SetDialogGUIPosition(Vector2 pos)
    {
        dialogWindow.transform.position = pos + new Vector2(0,1);
    }

    public void SetDialogText(string text)
    {
        dialogText.text = text;
    }


    public void StartFadeIn()
    {

    }

    public void StartFadeOut()
    {

    }
}
