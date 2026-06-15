using System.Collections;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float endCreditsDelay;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(RollCredits());

        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public void Menu()
    {
        LevelLoader.Instance.LoadLevel(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator RollCredits()
    {
        yield return new WaitForSecondsRealtime(endCreditsDelay);
    }
}
