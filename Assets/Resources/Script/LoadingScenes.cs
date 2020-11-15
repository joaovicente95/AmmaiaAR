using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Class for change scenes
/// </summary>
public class LoadingScenes : MonoBehaviour
{

    #region Variables

    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    #endregion //Variables
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="indexScene"></param>
    public void LoadScene(int indexScene)
    {
        StartCoroutine(LoadS(indexScene));
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sceneIndex"></param>
    /// <returns></returns>
    private IEnumerator LoadS(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);
        float progress;
        while (!operation.isDone)
        {
            progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }        
    }

}
