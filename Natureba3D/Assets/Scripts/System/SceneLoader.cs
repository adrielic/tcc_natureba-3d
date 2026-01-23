using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    Animator animator;
    public float transitionTime;

    public static SceneLoader Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void LoadScene(int sceneIndex)
    {
        if (sceneIndex < 0 || sceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError($"Invalid scene index: {sceneIndex}.");
            return;
        }

        StartCoroutine(PlaySceneTransition(sceneIndex));
    }

    IEnumerator PlaySceneTransition(int sceneIndex)
    {
        yield return new WaitForSecondsRealtime(transitionTime);

        SceneManager.LoadScene(sceneIndex);
    }
}
