using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public AudioSource audioSource;
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadSelectedScene(string sceneName)
    {
        // SceneManager.LoadScene(sceneName);
        StartCoroutine(SceneDelayCoroutine(sceneName));
    }

    IEnumerator SceneDelayCoroutine(string sceneName)
    {
        audioSource.GetComponent<AudioSource>();
        audioSource.volume = audioSource.volume * 0.6f;
        yield return new WaitForSecondsRealtime(0.1f);
        SceneManager.LoadScene(sceneName);
    }
}
