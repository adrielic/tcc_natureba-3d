using System;
using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Objectives")]
    public int requiredFood;
    public int requiredWater;
    public int requiredMedicine;
    [HideInInspector] public int foodCount = 0, waterCount = 0, medicineCount = 0;
    public bool objectiveIsComplete;

    [Header("Level Countdown")]
    public int timeLimit;
    public bool startCountdown = false;
    private Coroutine countdown;
    private int levelDuration;

    [Header("System")]
    [SerializeField] private bool isGameOver;
    public bool isPaused;
    private bool isMapOpen;

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

    void Start()
    {
        levelDuration = timeLimit;
    }

    void Update()
    {
        if (startCountdown && countdown == null)
        {
            countdown = StartCoroutine(LevelCountdown());

            Debug.Log($"Starting Day {SceneManager.GetActiveScene().buildIndex}.");
        }
        else if (!startCountdown && countdown != null)
        {
            StopCoroutine(countdown);
            countdown = null;
        }

        if (Input.GetButtonDown("Map") && !isGameOver)
        {
            isMapOpen = !isMapOpen;
            GameUIManager.Instance.HandleMap(isMapOpen);
        }

        if (Input.GetButtonDown("Pause/Unpause") && !isGameOver)
        {
            if (isPaused)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame(true);
            }
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
            objectiveIsComplete = true;
        }
    }

    public void FinishLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneLoader.Instance.LoadScene(nextSceneIndex);

        Debug.Log($"Proceeding to next level (Day {nextSceneIndex}).");
    }

    IEnumerator LevelCountdown()
    {
        while (levelDuration > 0)
        {
            yield return new WaitForSeconds(1);

            levelDuration--;
        }

        GameOver("Night");
        countdown = null;
    }

    public void GameOver(string causeOfDeath)
    {
        isGameOver = true;
        PauseGame(false);

        switch (causeOfDeath)
        {
            case "Night":
                GameUIManager.Instance.ShowGameOverPanel(causeOfDeath, "Na floresta, a noite é perigosa. Sem abrigo, as chances de sobrevivência caem drasticamente.");
                break;
            case "Falling":
                GameUIManager.Instance.ShowGameOverPanel(causeOfDeath, "Acidentes em terrenos irregulares são uma das principais causas de morte em áreas selvagens.");
                break;
            case "Drowning":
                GameUIManager.Instance.ShowGameOverPanel(causeOfDeath, "Correntes de rios podem ser traiçoeiras, mesmo em águas aparentemente calmas.");
                break;
            case "Intoxication_Fish":
                GameUIManager.Instance.ShowGameOverPanel(causeOfDeath, "Consumir peixe cru sem tratá-lo corretamente, pode causar intoxicações graves por parasitas e bactérias.");
                break;
            case "Intoxication_Mushroom":
                GameUIManager.Instance.ShowGameOverPanel(causeOfDeath, "Na natureza, quanto mais colorido for um cogumelo, maiores a chances de ser venenoso.");
                break;
            case "Animal":
                GameUIManager.Instance.ShowGameOverPanel(causeOfDeath, "Na natureza, aproximar-se de animais selvagens é um grande risco. Respeitar o espaço deles é essencial.");
                break;
        }

        if (countdown != null)
        {
            StopCoroutine(countdown);
        }

        Debug.Log($"The player is dead ({causeOfDeath}).");
    }

    public void PauseGame(bool showPausePanel)
    {
        if (isPaused) return;

        isPaused = true;
        Time.timeScale = 0;

        GameUIManager.Instance.HandlePausePanel(showPausePanel);
    }

    public void UnpauseGame()
    {
        if (!isPaused) return;

        isPaused = false;
        Time.timeScale = 1;

        GameUIManager.Instance.HandlePausePanel(false);
    }
}
