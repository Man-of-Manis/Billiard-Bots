using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenBar : MonoBehaviour
{
    public Image bar;

    public TMPro.TMP_Text text;

    [SerializeField] private float levelLoadDelay = 3f;

    public void Start()
    {
        LoadLevel(2);
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        bool load = false;

        yield return new WaitForSeconds(0.1f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        operation.allowSceneActivation = false;

        while (!load)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            if(bar != null && text != null)
            {
                bar.fillAmount = progress;

                text.text = Mathf.RoundToInt( progress * 100f) + "%";
            }       
            
            if(operation.progress >= 0.9f)
            {
                load = true;
            }

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(levelLoadDelay);

        operation.allowSceneActivation = true;
    }
}
