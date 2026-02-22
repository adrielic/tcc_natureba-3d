using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScreen : MonoBehaviour
{
    public void Play()
    {
        LevelLoader.Instance.LoadLevel(GameData.Load());
    }

    public void Quit()
    {
        Application.Quit();
    }
}
