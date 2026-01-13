using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [Header("Level Objectives")]
    public bool objectiveWasCompleted;
    public int foodNeeded, waterNeeded, medicineNeeded;
    [HideInInspector] public int foodCount = 0, waterCount = 0, medicineCount = 0;

    [Header("Level Timer")]
    public int totalDayTime;
    public bool dayHasStarted = false;
    Coroutine dayTime;

    [Header("Game Over")]
    public bool isGameOver;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        GameData.Save(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        if (dayHasStarted && dayTime == null)
        {
            dayTime = StartCoroutine(DayTime());

            Debug.Log($"Starting Day {SceneManager.GetActiveScene().buildIndex}.");
        }
        else if (!dayHasStarted && dayTime != null)
        {
            StopCoroutine(dayTime);
            dayTime = null;
        }
    }

    public void GameOver(string causeOfDeath)
    {
        switch (causeOfDeath)
        {
            case "Night":
                GameUIManager.Instance.UpdateDeathScreen(causeOfDeath, "Na floresta, a noite é perigosa. Sem abrigo, as chances de sobrevivência caem drasticamente.");
                break;
            case "Falling":
                GameUIManager.Instance.UpdateDeathScreen(causeOfDeath, "Acidentes em terrenos irregulares são uma das principais causas de morte em áreas selvagens.");
                break;
            case "Drowning":
                GameUIManager.Instance.UpdateDeathScreen(causeOfDeath, "Correntes de rios podem ser traiçoeiras, mesmo em águas aparentemente calmas.");
                break;
            case "Intoxication_Fish":
                GameUIManager.Instance.UpdateDeathScreen(causeOfDeath, "Consumir peixe cru sem tratá-lo corretamente, pode causar intoxicações graves por parasitas e bactérias.");
                break;
            case "Intoxication_Mushroom":
                GameUIManager.Instance.UpdateDeathScreen(causeOfDeath, "Na natureza, quanto mais colorido for um cogumelo, maiores a chances de ser venenoso.");
                break;
            case "Animal":
                GameUIManager.Instance.UpdateDeathScreen(causeOfDeath, "Na natureza, aproximar-se de animais selvagens é um grande risco. Respeitar o espaço deles é essencial.");
                break;
        }

        isGameOver = true;

        Debug.Log($"The player is dead ({causeOfDeath}).");
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
            case 'm':
                medicineCount++;
                break;
        }

        if (foodCount == foodNeeded && waterCount == waterNeeded && medicineCount == medicineNeeded)
            objectiveWasCompleted = true;
    }

    IEnumerator DayTime()
    {
        while (totalDayTime > 0)
        {
            yield return new WaitForSeconds(1);
            totalDayTime--;
        }

        GameOver("Night");
        dayTime = null;
    }
}
