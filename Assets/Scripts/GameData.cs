using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public enum EditModes
    {
        IDLE,
        MOVING_BUILDING,
        LIFT_BUILDING,
        MODIFY_LENGTH,
        MODIFY_WIDTH,
        MODIFY_HEIGHT,
        ASSIGN_MATERIAL
    }

    public static GameData gameData;
    [SerializeField] private float moneyLimit = 0;
    [SerializeField] private float co2Limit = 0;
    [SerializeField] private int buildingLimit = 0;
    [SerializeField] private GameObject selectedObject;
    [SerializeField] private EditModes editMode = EditModes.IDLE;

    public EditModes EditMode
    {
        get { return editMode; }
        set { editMode = value; }
    }

    public float MoneyLimit
    {
        get { return moneyLimit; }
        set { moneyLimit = value; }
    }

    public float CO2Limit
    {
        get { return co2Limit; }
        set { co2Limit = value; }
    }

    public int BuildingLimit
    {
        get { return buildingLimit; }
        set { buildingLimit = value; }
    }

    public GameObject SelectedObject
    {
        get { return selectedObject; }
        set { selectedObject = value; }
    }

    private void Awake()
    {
        Singleton();
    }

    void Singleton()
    {
        if (GameData.gameData == null)
        {
            GameData.gameData = this;
        }
        else
        {
            if (GameData.gameData != this)
            {
                Destroy(GameData.gameData.gameObject);
                GameData.gameData = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
