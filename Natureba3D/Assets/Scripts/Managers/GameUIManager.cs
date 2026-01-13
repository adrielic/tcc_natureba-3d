using System.Collections;
using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public GameObject hud, deathScreen;
    public TMP_Text interactionTxt, feedbackTxt, foodTxt, waterTxt, medicineTxt, deathMessageTxt;

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
        StartCoroutine(ChangeFeedbackText(null));
        UpdateObjetiveDisplay('n');
    }

    public void ChangeInteractionText(string newText)
    {
        interactionTxt.text = newText;
    }

    public IEnumerator ChangeFeedbackText(string newText)
    {
        feedbackTxt.text = newText;
        yield return new WaitForSeconds(1);
        feedbackTxt.text = null;
    }

    public void UpdateObjetiveDisplay(char objective)
    {
        switch (objective)
        {
            case 'f':
                foodTxt.text = "Comida: " + GameManager.Instance.foodCount + "/" + GameManager.Instance.foodNeeded;
                break;
            case 'w':
                waterTxt.text = "Água: " + GameManager.Instance.waterCount + "/" + GameManager.Instance.waterNeeded;
                break;
            case 'm':
                medicineTxt.text = "Medicina: " + GameManager.Instance.medicineCount + "/" + GameManager.Instance.medicineNeeded;
                break;
            case 'n':
                foodTxt.text = "Comida: " + GameManager.Instance.foodCount + "/" + GameManager.Instance.foodNeeded;
                waterTxt.text = "Água: " + GameManager.Instance.waterCount + "/" + GameManager.Instance.waterNeeded;
                medicineTxt.text = "Medicina: " + GameManager.Instance.medicineCount + "/" + GameManager.Instance.medicineNeeded;
                break;
        }
    }

    public void DeactivateHUD()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        hud.SetActive(false);
    }

    public void ActivateHUD()
    {
        Cursor.lockState = CursorLockMode.Locked;
        hud.SetActive(true);
    }

    public void UpdateDeathScreen(string causeOfDeath, string newText)
    {
        DeactivateHUD();
        deathScreen.SetActive(true);

        deathMessageTxt.text = newText;
    }

    public void RestartButton()
    {
        SceneLoader.Instance.LoadScene(GameData.Load());
    }
}
