using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private Textures textures;
    [SerializeField] private TextHandler textHandler;
    [SerializeField] private GameObject buildingBase;
    [SerializeField] private float zoomingScale = 5f;
    private Vector3 cameraForward;
    
    private void Awake()
    {
        cameraForward = Camera.main.transform.forward;
    }

    public void CreateBase()
    {
        GameObject myBuilding = Instantiate(buildingBase);
        myBuilding.transform.position = Vector3.zero;
    }

    public void SetMovingMode()
    {
        GameData.gameData.EditMode = GameData.EditModes.MOVING_BUILDING;
        textHandler.setModeText("Moving Building");
    }

    public void SetLiftingMode()
    {
        GameData.gameData.EditMode = GameData.EditModes.LIFT_BUILDING;
        textHandler.setModeText("Lifting Building");
    }

    public void SetModifyLength()
    {
        GameData.gameData.EditMode = GameData.EditModes.MODIFY_LENGTH;
        textHandler.setModeText("Modifying Length");
    }

    public void SetModifyWidth()
    {
        GameData.gameData.EditMode = GameData.EditModes.MODIFY_WIDTH;
        textHandler.setModeText("Modifying Width");
    }

    public void SetModifyHeight()
    {
        GameData.gameData.EditMode = GameData.EditModes.MODIFY_HEIGHT;
        textHandler.setModeText("Modifying Height");
    }

    public void SetAssignMaterial()
    {
        GameData.gameData.EditMode = GameData.EditModes.ASSIGN_MATERIAL;
        textHandler.setModeText("Assigning Material");
    }

    public void ZoomIn()
    {
        Camera.main.transform.localPosition += cameraForward * zoomingScale;
    }

    public void ZoomOut()
    {
        Camera.main.transform.localPosition -= cameraForward * zoomingScale;
    }

    public void Unselect()
    {
        GameData.gameData.SelectedObject.GetComponent<ClickTester>().UndoOutline();
        GameData.gameData.SelectedObject = null;
        GameData.gameData.EditMode = GameData.EditModes.IDLE;
        textHandler.setModeText("Select Object");
    }

    public void AssignWoodTexture()
    {
        if (GameData.gameData.SelectedObject == null) return;
        GameData.gameData.SelectedObject.GetComponent<Renderer>().material.mainTexture = textures.woodTexture;
    }

    public void AssignWoodFloorTexture()
    {
        if (GameData.gameData.SelectedObject == null) return;
        GameData.gameData.SelectedObject.GetComponent<Renderer>().material.mainTexture = textures.woodFloorTexture;
    }

    public void AssignBrickTexture()
    {
        if (GameData.gameData.SelectedObject == null) return;
        GameData.gameData.SelectedObject.GetComponent<Renderer>().material.mainTexture = textures.brickTexture;
    }

    public void AssignConcreteTexture()
    {
        if (GameData.gameData.SelectedObject == null) return;
        GameData.gameData.SelectedObject.GetComponent<Renderer>().material.mainTexture = textures.concreteTexture;
    }
}
