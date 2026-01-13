using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScreen : MonoBehaviour
{
    public void Play()
    {
        //SceneLoader.Instance.LoadScene(GameData.Load());
        SceneManager.LoadScene(GameData.Load());
    }

    public void Quit()
    {
        Application.Quit();
    }
}
