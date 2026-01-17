using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Objectives")]
    public int requiredFood;
    public int requiredWater;
    public int requiredMedicine;
    [HideInInspector] public int foodCount = 0, waterCount = 0, medicineCount = 0;
    public bool objectiveComplete;

    [Header("Day/Night Timer")]
    public int totalDayTime;
    public bool dayHasStarted = false;
    Coroutine dayTime;

    [Header("System")]
    public bool isGameOver;
    public bool isPaused;
    bool isMapOpen;

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

        if (Input.GetButtonDown("Map") && !isGameOver)
        {
            isMapOpen = !isMapOpen;
            GameUIManager.Instance.HandleMap(isMapOpen);
        }

        if (Input.GetButtonDown("Pause/Unpause") && !isGameOver)
        {
            HandlePause();
        }
    }

    public void CheckObjective(string objective)
    {
        switch (objective)
        {
            case "food":
                foodCount++;
                break;
            case "water":
                waterCount++;
                break;
            case "medicine":
                medicineCount++;
                break;
        }

        GameUIManager.Instance.UpdateObjetiveDisplay(objective);

        if (foodCount == requiredFood && waterCount == requiredWater && medicineCount == requiredMedicine)
        {
            objectiveComplete = true;
        }
    }

    public void FinishLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneLoader.Instance.LoadScene(nextSceneIndex);

        Debug.Log($"Proceeding to next level (Day {nextSceneIndex}).");
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

    public void GameOver(string causeOfDeath)
    {
        switch (causeOfDeath)
        {
            case "Night":
                GameUIManager.Instance.ShowGameOver(causeOfDeath, "Na floresta, a noite é perigosa. Sem abrigo, as chances de sobrevivência caem drasticamente.");
                break;
            case "Falling":
                GameUIManager.Instance.ShowGameOver(causeOfDeath, "Acidentes em terrenos irregulares são uma das principais causas de morte em áreas selvagens.");
                break;
            case "Drowning":
                GameUIManager.Instance.ShowGameOver(causeOfDeath, "Correntes de rios podem ser traiçoeiras, mesmo em águas aparentemente calmas.");
                break;
            case "Intoxication_Fish":
                GameUIManager.Instance.ShowGameOver(causeOfDeath, "Consumir peixe cru sem tratá-lo corretamente, pode causar intoxicações graves por parasitas e bactérias.");
                break;
            case "Intoxication_Mushroom":
                GameUIManager.Instance.ShowGameOver(causeOfDeath, "Na natureza, quanto mais colorido for um cogumelo, maiores a chances de ser venenoso.");
                break;
            case "Animal":
                GameUIManager.Instance.ShowGameOver(causeOfDeath, "Na natureza, aproximar-se de animais selvagens é um grande risco. Respeitar o espaço deles é essencial.");
                break;
        }

        isGameOver = true;

        if (dayTime != null)
        {
            StopCoroutine(dayTime);
        }

        Debug.Log($"The player is dead ({causeOfDeath}).");
    }

    public void HandlePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;

        if (isPaused)
        {
            GameUIManager.Instance.OpenPausePanel();
        }
        else
        {
            GameUIManager.Instance.ClosePausePanel();
        }
    }
}
