using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject map;
    [SerializeField] private TMP_Text interactionTxt;
    [SerializeField] private TMP_Text notificationTxt;
    [SerializeField] private Image foodBar;
    [SerializeField] private Image waterBar;
    [SerializeField] private Image medicineBar;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text deathMessageTxt;

    [Header("Pause")]
    [SerializeField] private GameObject pausePanel;

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

    public void ShowGameOverPanel(string causeOfDeath, string newText)
    {
        gameOverPanel.SetActive(true);
        deathMessageTxt.text = newText;
        HandleHUD(false);
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
