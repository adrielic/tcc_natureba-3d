using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScreen : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(GameData.Load());
    }

    public void Quit()
    {
        Application.Quit();
    }
}
