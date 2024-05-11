using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadSceneController : MonoBehaviour
{
    static string nextScene;

    [SerializeField]
    Slider progressBar;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    private void Start()
    {
        StartCoroutine(LoadSceneProgress());
    }

    IEnumerator LoadSceneProgress()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            yield return null;

            if(progressBar.value < 0.9f)
                progressBar.value = Mathf.MoveTowards(progressBar.value, 0.9f, Time.deltaTime);
            else
                progressBar.value = Mathf.MoveTowards(progressBar.value, 1f, Time.deltaTime);


            if (progressBar.value >= 1f && op.progress >= 0.9f)
                op.allowSceneActivation = true;
        }
    }
}
