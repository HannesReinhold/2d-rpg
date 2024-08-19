using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GUIManager GuiManager;

    private void Awake()
    {
        

        if(Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnterLevel(string levelName, int targetID)
    {
        StartCoroutine(LoadSceneAsnyc(levelName, targetID));
    }

    private IEnumerator LoadSceneAsnyc(string sceneID, int targetID)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneID);
        FadeOut();

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        DoorInteractable[] availableDoors = FindObjectsOfType<DoorInteractable>();
        Debug.Log("FOund " + availableDoors.Length);
        for(int i=0; i<availableDoors.Length; i++)
        {
            if (availableDoors[i].doorID == targetID) TeleportPlayer(availableDoors[i].spawn.position);
        }
        FadeIn();
    }

    public void TeleportPlayer(Vector2 pos)
    {
        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        Debug.Log(player);
        if(player != null) player.transform.position = pos;
    }

    public void FadeOut()
    {
        GuiManager.StartFadeOut();
    }

    public void FadeIn()
    {
        GuiManager.StartFadeIn();
    }
}
