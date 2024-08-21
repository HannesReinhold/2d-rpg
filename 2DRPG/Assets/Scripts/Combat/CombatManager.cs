using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public CombatGUI gui;
    private bool enableInput;


    private void Awake()
    {
    }

    private void Update()
    {
        if (!enableInput) return;
        if(Input.GetKeyDown(KeyCode.Escape)) EndCombat();
    }

    public void StartCombat()
    {
        GameManager.Instance.DisableMovement();
        enableInput = true;
        StartCoroutine(FadeToCombat());
    }

    private IEnumerator FadeToCombat()
    {
        yield return new WaitForSeconds(1);
        GameManager.Instance.guiManager.CloseHint();

        GameManager.Instance.FadeIn();
        yield return new WaitForSeconds(0.5f);

        gui.Open();
        GameManager.Instance.FadeOut();
        yield return new WaitForSeconds(0.5f);

        enableInput = true;
    }

    public void EndCombat()
    {
        GameManager.Instance.EnableMovement();
        enableInput = false;
        StartCoroutine(FadeOutCombat());
    }

    private IEnumerator FadeOutCombat()
    {
        GameManager.Instance.FadeIn();
        yield return new WaitForSeconds(0.5f);
        gui.Close();
        GameManager.Instance.FadeOut();
        yield return new WaitForSeconds(0.5f);
        enableInput = false;
    }
}
