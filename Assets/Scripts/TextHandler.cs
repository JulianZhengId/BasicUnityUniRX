using UnityEngine;
using TMPro;

public class TextHandler : MonoBehaviour
{
    //Building
    [SerializeField] private TextMeshProUGUI mode;
    [SerializeField] private TextMeshProUGUI buildingLength;
    [SerializeField] private TextMeshProUGUI buildingWidth;
    [SerializeField] private TextMeshProUGUI buildingHeight;
    [SerializeField] private TextMeshProUGUI buildingCost;
    [SerializeField] private TextMeshProUGUI buildingCO2;
    [SerializeField] private TextMeshProUGUI buildingPosX;
    [SerializeField] private TextMeshProUGUI buildingPosY;
    [SerializeField] private TextMeshProUGUI buildingPosZ;

    //Personal
    [SerializeField] private TextMeshProUGUI personalMoney;
    [SerializeField] private TextMeshProUGUI personalCO2;
    [SerializeField] private TextMeshProUGUI personalBuilding;

    //Remaining
    [SerializeField] private TextMeshProUGUI remainingMoney;
    [SerializeField] private TextMeshProUGUI remainingCO2;


    private void Awake()
    {
        setModeText("Select Object");
        setPersonalMoneyText(GameData.gameData.MoneyLimit.ToString());
        setPersonalCO2Text(GameData.gameData.CO2Limit.ToString());
        setPersonalBuildingText(GameData.gameData.BuildingLimit.ToString());
        setRemainingMoneyText(GameData.gameData.MoneyLimit.ToString());
        setRemainingCO2Text(GameData.gameData.CO2Limit.ToString());
    }

    public void setModeText(string s)
    {
        mode.text = "Mode : " + s;
    }

    public void setBuildingLengthText(string s)
    {
        buildingLength.text = ". Length	: " + s;
    }

    public void setBuildingWidthText(string s)
    {
        buildingWidth.text = ". Width	: " + s;
    }

    public void setBuildingHeightText(string s)
    {
        buildingHeight.text = ". Height	: " + s;
    }

    public void setBuildingCostText(string s)
    {
        buildingCost.text += s;
    }

    public void setBuildingCO2Text(string s)
    {
        buildingCO2.text += s;
    }

    public void setAttributesText(Vector3 v)
    {
        setBuildingLengthText(v.x.ToString());
        setBuildingWidthText(v.z.ToString());
        setBuildingHeightText(v.y.ToString());
    }

    public void setPositionText(Vector3 v)
    {
        buildingPosX.text = ". X : " + v.x.ToString();
        buildingPosY.text = ". Y : " + v.y.ToString();
        buildingPosZ.text = ". Z : " + v.z.ToString();
    }

    private void setPersonalMoneyText(string s)
    {
        personalMoney.text = "Max Money : " + s;
    }

    private void setPersonalCO2Text(string s)
    {
        personalCO2.text = "Max CO2    : " + s;
    }

    private void setPersonalBuildingText(string s)
    {
        personalBuilding.text = "Building    : " + s;
    }

    public void setRemainingMoneyText(string s)
    {
        remainingMoney.text = "Money : " + s;
    }

    public void setRemainingCO2Text(string s)
    {
        remainingCO2.text = "CO2    : " + s;
    }
}
