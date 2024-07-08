using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
  public class SceneLoader
  {
    private readonly ICoroutineRunner _coroutineRunner;

    public SceneLoader(ICoroutineRunner coroutineRunner) => 
      _coroutineRunner = coroutineRunner;

    public void Load(string name, Action onLoaded = null)
    {
      _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
    }

    public IEnumerator LoadScene(string nextScene, Action onLoaded = null)
    {
      // Debug.Log($"Testing LoadScene nextScene {nextScene}");
      if (SceneManager.GetActiveScene().name == nextScene)
      {
        // Debug.Log($"Testing LoadScene SceneManager.GetActiveScene().name == nextScene");
        onLoaded?.Invoke();
        // _coroutineRunner.StopAllCoroutines();
        yield break;
      }
      // Debug.Log($"Testing LoadScene before waitNextScene");
      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);
#if UNITY_EDITOR
      // Debug.Log($"Testing LoadScene after waitNextScene");
      while (!waitNextScene.isDone)
      {
        // Debug.Log($"Testing LoadScene !waitNextScene.isDone");
        yield return null;
      }
      // Debug.Log($"Testing LoadScene End");
      onLoaded?.Invoke();
      // _coroutineRunner.StopAllCoroutines();
#else
      waitNextScene.completed += operation =>
      {
        onLoaded?.Invoke();
        Debug.Log($"Testing LoadScene waitNextScene.completed");
        waitNextScene.completed += null;
      };
#endif
    }
  }
}