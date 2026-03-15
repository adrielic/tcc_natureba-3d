using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatTable : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                LevelLoader.Instance.LoadLevel(1);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                LevelLoader.Instance.LoadLevel(2);

            if (Input.GetKeyDown(KeyCode.Alpha3))
                LevelLoader.Instance.LoadLevel(3);

            if (Input.GetKeyDown(KeyCode.Alpha4))
                LevelLoader.Instance.LoadLevel(4);

            if (Input.GetKeyDown(KeyCode.Alpha5))
                LevelLoader.Instance.LoadLevel(5);

            if (Input.GetKeyDown(KeyCode.Alpha6))
                LevelLoader.Instance.LoadLevel(6);

            if (Input.GetKeyDown(KeyCode.Alpha7))
                LevelLoader.Instance.LoadLevel(7);

            if (Input.GetKeyDown(KeyCode.RightArrow))
                NextLevel();

            if (Input.GetKeyDown(KeyCode.LeftArrow))
                PreviousLevel();
        }
    }

    void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        LevelLoader.Instance.LoadLevel(nextSceneIndex);
    }

    void PreviousLevel()
    {
        int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        LevelLoader.Instance.LoadLevel(previousSceneIndex);
    }
}
