using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public TMP_Text interactionTxt, feedbackTxt, foodTxt, waterTxt, healthTxt;

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
            case 'h':
                healthTxt.text = "Medicina: " + GameManager.Instance.healthCount + "/" + GameManager.Instance.healthNeeded;
                break;
            case 'n':
                foodTxt.text = "Comida: " + GameManager.Instance.foodCount + "/3";
                waterTxt.text = "Água: " + GameManager.Instance.waterCount + "/1";
                healthTxt.text = "Medicina: " + GameManager.Instance.healthCount + "/1";
                break;
        }
    }
}
