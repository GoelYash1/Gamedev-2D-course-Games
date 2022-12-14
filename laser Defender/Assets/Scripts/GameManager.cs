using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
    using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    public void LoadGame(float delay)
    {
        if(scoreKeeper!=null)
        {
            scoreKeeper.ResetScore();
        }
        StartCoroutine(WaitAndLoad("GameScreen",delay));
    }
    public void LoadMainMenu(float delay)
    {
        StartCoroutine(WaitAndLoad("MainScreen",delay));
    }
    public void LoadEndScreen(float delay)
    {
        StartCoroutine(WaitAndLoad("End Screen",delay));
    }
    public void Quit()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
