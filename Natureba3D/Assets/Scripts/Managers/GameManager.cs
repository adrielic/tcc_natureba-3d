using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool objectiveWasCompleted;
    public int foodNeeded, waterNeeded, healthNeeded;
    [HideInInspector] public int foodCount = 0, waterCount = 0, healthCount = 0;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        GameData.levelIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("currentLevelIndex", GameData.levelIndex);
        PlayerPrefs.Save();
    }

    public void GameOver(string causeOfDeath)
    {

    }

    public void UpdateObjective(char objective)
    {
        switch (objective)
        {
            case 'f':
                foodCount++;
                break;
            case 'w':
                waterCount++;
                break;
            case 'h':
                healthCount++;
                break;
        }

        if (foodCount == foodNeeded && waterCount == waterNeeded && healthCount == healthNeeded)
            objectiveWasCompleted = true;
    }
}
