using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject map;
    [SerializeField] private TMP_Text journalTxt;
    [SerializeField] private TMP_Text interactionTxt;
    [SerializeField] private TMP_Text notificationTxt;
    [SerializeField] private Image foodBar;
    [SerializeField] private Image waterBar;
    [SerializeField] private Image medicineBar;
    [SerializeField] private Image staminaBar;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text deathMessageTxt;
    [SerializeField] private Image gameOverBG;
    [SerializeField] private Sprite gameOverDrowningBG;
    [SerializeField] private Sprite gameOverAnimalBG;
    [SerializeField] private Sprite gameOverNightBG;
    [SerializeField] private Sprite gameOverFallingBG;
    [SerializeField] private Sprite gameOverIntoxFishBG;
    [SerializeField] private Sprite gameOverIntoxMushroomBG;

    [Header("Pause")]
    [SerializeField] private GameObject pausePanel;

    [Header("Level Intro")]
    [SerializeField] private TMP_Text titleTxt;
    [SerializeField] private TMP_Text subtitleTxt;

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
        StartCoroutine(ShowNotification(null));
        UpdateObjetiveDisplay("none");
    }

    public void ShowInteraction(string newText)
    {
        interactionTxt.text = newText;
    }

    public IEnumerator ShowNotification(string newText)
    {
        notificationTxt.text = newText;

        yield return new WaitForSeconds(1);

        notificationTxt.text = null;
    }

    public void UpdateObjetiveDisplay(string objective)
    {
        switch (objective)
        {
            case "food":
                foodBar.fillAmount = (float)GameManager.Instance.foodCount / GameManager.Instance.requiredFood;
                break;
            case "water":
                waterBar.fillAmount = (float)GameManager.Instance.waterCount / GameManager.Instance.requiredWater;
                break;
            case "medicine":
                medicineBar.fillAmount = (float)GameManager.Instance.medicineCount / GameManager.Instance.requiredMedicine;
                break;
            case "none":
                foodBar.fillAmount = (float)GameManager.Instance.foodCount / GameManager.Instance.requiredFood;
                waterBar.fillAmount = (float)GameManager.Instance.waterCount / GameManager.Instance.requiredWater;
                medicineBar.fillAmount = (float)GameManager.Instance.medicineCount / GameManager.Instance.requiredMedicine;
                break;
        }
    }

    public void UpdateJournalText(string newText)
    {
        journalTxt.text = newText;
    }

    public void UpdateStaminaBar(float currentValue, float maxValue)
    {
        staminaBar.fillAmount = currentValue / maxValue;
    }

    public void ShowGameOverPanel(string causeOfDeath, string newText)
    {
        HandleHUD(false);
        gameOverPanel.SetActive(true);
        deathMessageTxt.text = newText;

        switch (causeOfDeath)
        {
            case "Night":
                gameOverBG.sprite = gameOverNightBG;
                break;
            case "Falling":
                gameOverBG.sprite = gameOverFallingBG;
                break;
            case "Drowning":
                gameOverBG.sprite = gameOverDrowningBG;
                break;
            case "Intoxication_Fish":
                gameOverBG.sprite = gameOverIntoxFishBG;
                break;
            case "Intoxication_Mushroom":
                gameOverBG.sprite = gameOverIntoxMushroomBG;
                break;
            case "Animal":
                gameOverBG.sprite = gameOverAnimalBG;
                break;
        }
    }

    public void HandleMap(bool showMap)
    {
        map.SetActive(showMap);
        HandleHUD(!showMap);
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

    public void UpdateTitle(string newTitle, string newSubtitle)
    {
        // titleTxt.text = newTitle;
        // subtitleTxt.text = newSubtitle;
        StartCoroutine(WriteText(newTitle, newSubtitle));
    }

    IEnumerator WriteText(string mainText, string altText)
    {
        foreach (char word in mainText)
        {
            yield return new WaitForSecondsRealtime(0.1f);

            titleTxt.text += word;
        }

        foreach (char word in altText)
        {
            yield return new WaitForSecondsRealtime(0.1f);

            subtitleTxt.text += word;
        }
    }

    public void Resume()
    {
        GameManager.Instance.UnpauseGame();
    }

    public void Restart()
    {
        GameManager.Instance.UnpauseGame();
        LevelLoader.Instance.LoadLevel(GameData.Load());
    }

    public void ReturnToMenu()
    {
        LevelLoader.Instance.LoadLevel(0);
    }
}
