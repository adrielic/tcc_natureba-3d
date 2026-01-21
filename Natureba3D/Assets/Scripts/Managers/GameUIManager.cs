using System.Collections;
using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [Header("HUD")]
    public GameObject hud;
    public GameObject map;
    public TMP_Text interactionTxt, feedbackTxt, foodTxt, waterTxt, medicineTxt;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public TMP_Text deathMessageTxt;

    [Header("Pause")]
    public GameObject pausePanel;

    public static GameUIManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        StartCoroutine(ShowFeedback(null));
        UpdateObjetiveDisplay("none");
    }

    public void ShowInteraction(string newText)
    {
        interactionTxt.text = newText;
    }

    public IEnumerator ShowFeedback(string newText)
    {
        feedbackTxt.text = newText;

        yield return new WaitForSeconds(1);

        feedbackTxt.text = null;
    }

    public void UpdateObjetiveDisplay(string objective)
    {
        switch (objective)
        {
            case "food":
                foodTxt.text = "Comida: " + GameManager.Instance.foodCount + "/" + GameManager.Instance.requiredFood;
                break;
            case "water":
                waterTxt.text = "Água: " + GameManager.Instance.waterCount + "/" + GameManager.Instance.requiredWater;
                break;
            case "medicine":
                medicineTxt.text = "Medicina: " + GameManager.Instance.medicineCount + "/" + GameManager.Instance.requiredMedicine;
                break;
            case "none":
                foodTxt.text = "Comida: " + GameManager.Instance.foodCount + "/" + GameManager.Instance.requiredFood;
                waterTxt.text = "Água: " + GameManager.Instance.waterCount + "/" + GameManager.Instance.requiredWater;
                medicineTxt.text = "Medicina: " + GameManager.Instance.medicineCount + "/" + GameManager.Instance.requiredMedicine;
                break;
        }
    }

    public void ShowGameOverPanel(string causeOfDeath, string newText)
    {
        gameOverPanel.SetActive(true);
        deathMessageTxt.text = newText;
        HandleHUD(false);
    }

    public void HandleMap(bool showMap)
    {
        map.SetActive(showMap);
    }

    public void HandleHUD(bool showHUD)
    {
        hud.SetActive(showHUD);

        if (showHUD)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void HandlePausePanel(bool showPausePanel)
    {
        pausePanel.SetActive(showPausePanel);
        HandleHUD(!showPausePanel);
    }

    public void Resume()
    {
        GameManager.Instance.UnpauseGame();
    }

    public void Restart()
    {
        GameManager.Instance.UnpauseGame();
        SceneLoader.Instance.LoadScene(GameData.Load());
    }

    public void ReturnToMenu()
    {
        SceneLoader.Instance.LoadScene(0);
    }
}
