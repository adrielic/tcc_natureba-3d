using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScreen : MonoBehaviour
{
    void Start()
    {
        GameData.levelIndex = PlayerPrefs.GetInt("currentLevelIndex", 1);
    }

    public void Play()
    {
        SceneManager.LoadScene(GameData.levelIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
