using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject moneyInput;
    [SerializeField] private GameObject co2Input;
    [SerializeField] private GameObject buildingInput;
    [SerializeField] private GameObject startGameButton;
    [SerializeField] private TextMeshProUGUI warningText;

    private void Awake()
    {
        warningText.text = "";
        StartCoroutine(activateInputField());
    }

    public void StartGame()
    {
        string moneyInputText = moneyInput.GetComponent<TMP_InputField>().text;
        string co2InputText = co2Input.GetComponent<TMP_InputField>().text;
        string buildingInputText = buildingInput.GetComponent<TMP_InputField>().text;

        if (moneyInputText == "" || co2InputText == "" || buildingInputText == "")
        {
            warningText.text = "Warning : Input cant be empty";
            return;
        }
        else if (!isNumber(moneyInputText) || !isNumber(co2InputText) || !isNumber(buildingInputText))
        {
            warningText.text = "Warning : All Input must be a number";
            return;
        }
        Debug.Log("Success");
        warningText.text = "Loading...";
        GameData.gameData.MoneyLimit = float.Parse(moneyInputText);
        GameData.gameData.CO2Limit = float.Parse(co2InputText);
        GameData.gameData.BuildingLimit = int.Parse(buildingInputText);
        SceneManager.LoadSceneAsync(1);
    }

    IEnumerator activateInputField()
    {
        yield return new WaitForSeconds(1.3f);
        moneyInput.SetActive(true);
        co2Input.SetActive(true);
        buildingInput.SetActive(true);
        startGameButton.SetActive(true);
    }

    private bool isNumber(string str)
    {
        return int.TryParse(str, out int n);
    }
}
