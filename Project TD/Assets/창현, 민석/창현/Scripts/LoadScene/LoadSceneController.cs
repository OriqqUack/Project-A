using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadSceneController : BaseScene
{
    [SerializeField]
    Slider progressBar;


    private void Start()
    {
        StartCoroutine(LoadSceneProgress());
    }

    IEnumerator LoadSceneProgress()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(Managers.Scene.NextSceneName);
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

    public override void Clear()
    {
        
    }
}
