using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    Animator animator;
    public float delay;

    public static LevelLoader Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        animator = GetComponent<Animator>();
    }
    
    public void LoadLevel(int sceneIndex)
    {
        if (sceneIndex < 0 || sceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError($"Invalid scene index: {sceneIndex}.");
            return;
        }

        StartCoroutine(PlayTransition(sceneIndex));
    }

    IEnumerator PlayTransition(int sceneIndex)
    {
        yield return new WaitForSecondsRealtime(delay);

        SceneManager.LoadScene(sceneIndex);
    }
}
