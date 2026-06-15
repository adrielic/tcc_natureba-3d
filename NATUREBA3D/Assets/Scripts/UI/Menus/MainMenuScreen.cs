using UnityEngine;
using TMPro;

public class MainMenuScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text versionNumberText;

    void Start()
    {
        versionNumberText.SetText($"v{Application.version}");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Play()
    {
        LevelLoader.Instance.LoadLevel(GameData.Load());
    }

    public void Quit()
    {
        Application.Quit();
    }
}
