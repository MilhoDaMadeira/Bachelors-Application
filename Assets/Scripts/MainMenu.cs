using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ViveSR;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(string sceneName)
    {
        StartCoroutine(LoadYourAsyncScene(sceneName));
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    void Start()
    {
        
        Lean.Localization.LeanLocalization.SetCurrentLanguageAll(LanguageSelect.language);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    static IEnumerator LoadYourAsyncScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

