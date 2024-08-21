using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour
{
    public Canvas dialogWindow;
    public TextMeshProUGUI dialogText;
    public CanvasGroup transitionCanvas;
    public GameObject hintCanvas;

    public bool isVisible;

    private void Awake()
    {
        SetDialogGUI(isVisible);
        DontDestroyOnLoad(gameObject);
        LeanTween.alphaCanvas(transitionCanvas, 0, 0);
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
        //LeanTween.alphaCanvas(transitionCanvas, 1, 0.5f);
        //transitionCanvas.alpha = 1;
        StartCoroutine(FadeTransitionCanvas(0.4f, 1));
    }

    public void StartFadeOut()
    {
        //LeanTween.alphaCanvas(transitionCanvas, 0, 0.5f);
        //transitionCanvas.alpha = 0;
        StartCoroutine(FadeTransitionCanvas(0.4f,0));
    }

    private IEnumerator FadeTransitionCanvas(float transitionTimeInSec, float alphaTarget)
    {
        float currentAlpha = transitionCanvas.alpha;
        float currentTime = 0;
        float t = 0;
        int numSteps = 8;
        int i = 0;
        float tInc = transitionTimeInSec != 0 ? 1f/transitionTimeInSec : 1;
        while(t<1)
        {
            currentTime += Time.deltaTime;
            i++;
            if(i>numSteps)
            {
                i = 0;
                t = tInc * currentTime;
            }
            Debug.Log(t);
            transitionCanvas.alpha = Mathf.Lerp(currentAlpha, alphaTarget, t);
           
            yield return null;
        }
    }

    public void OpenHint(Vector3 position)
    {
        hintCanvas.SetActive(true);
        hintCanvas.transform.position = position;
    }

    public void CloseHint()
    {
        hintCanvas.SetActive(false);
    }
}
