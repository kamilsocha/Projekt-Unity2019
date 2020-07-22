using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages fades between scenes.
/// </summary>
public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance;

    public Image image;
    public AnimationCurve curve;
    public string activeScene;

    AsyncOperation[] async = new AsyncOperation[2];
    public string sharedSceneName = "LevelShared";

    private void Start()
    {
        if(Instance != null)
        {
            Debug.Log("SceneFader gets destroyed");
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        image.enabled = true;
        activeScene = SceneManager.GetActiveScene().name;
        StartCoroutine(FadeIn());
    }

    /// <summary>
    /// Fades to provided scene using specified type of loading.
    /// Single scene loading when type equals LoadType.Menu -
    /// used when loading menu scenes.
    /// Single scene in additive mode when type is equal to LoadType.SingleLevel - 
    /// used to load another level without unloading levelShared.
    /// Two scenes of which one is always LevelShared and another one is chosen level -
    /// used when first entering playmode from menu.
    /// </summary>
    /// <param name="sceneToLoad">name of scene to load</param>
    /// <param name="type">type of loading</param>
    public void FadeTo(string sceneToLoad, LoadType type)
    {
        if(type == LoadType.Menu)
        {
            async[0] = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);
            async[0].allowSceneActivation = false;
        }
        else if(type == LoadType.SingleLevel)
        {
            async[0] = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
            async[0].allowSceneActivation = false;
        }
        else if(type == LoadType.DoubleLevel)
        {
            async[0] = SceneManager.LoadSceneAsync(sharedSceneName, LoadSceneMode.Single);
            async[1] = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
            async[0].allowSceneActivation = false;
            async[1].allowSceneActivation = false;
        }
        StartCoroutine(FadeOut(activeScene));

    }

    public void ReloadLevel()
    {
        async[0] = SceneManager.LoadSceneAsync(sharedSceneName, LoadSceneMode.Single);
        async[1] = SceneManager.LoadSceneAsync(activeScene, LoadSceneMode.Additive);
        async[0].allowSceneActivation = false;
        async[1].allowSceneActivation = false;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        float t = 1f;
        while(t > 0)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            image.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }

    IEnumerator FadeOut(string previousScene)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            image.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        SceneManager.UnloadSceneAsync(previousScene);
        if (async[1] != null) async[1].allowSceneActivation = true;
        if (async[0] != null) async[0].allowSceneActivation = true;

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeOut()
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            image.color = new Color(0f, 0f, 0f, a);
            yield return 0; 
        }
        if (async[1] != null)
            async[1].allowSceneActivation = true;
        if (async[0] != null)
            async[0].allowSceneActivation = true;

        StartCoroutine(FadeIn());
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != sharedSceneName)
        {
            activeScene = scene.name;
        }
    }

}


[System.Serializable]
public enum LoadType
{
    Menu,
    SingleLevel,
    DoubleLevel
}