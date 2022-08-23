using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] float waitToLoadNextLevel = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Finish"))
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if(nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
            {
                Debug.Log(SceneManager.sceneCountInBuildSettings);
                nextSceneIndex = 0;
            }
            StartCoroutine(LoadNextLevel(nextSceneIndex));
        }
    }
    IEnumerator LoadNextLevel(int nextScene)
    {
        yield return new WaitForSecondsRealtime(waitToLoadNextLevel);
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextScene);
    }
}
