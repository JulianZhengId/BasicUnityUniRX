using UnityEngine.UI;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private Textures textures;
    [SerializeField] private TextHandler textHandler;
    [SerializeField] private GameObject buildingBase;
    [SerializeField] private float zoomingScale = 5f;
    [SerializeField] private Slider zoomSlider;
    [SerializeField] private GameObject cameraPivot;
    private float camFOV;

    private void Start()
    {
        camFOV = Camera.main.fieldOfView;
        zoomSlider.onValueChanged.AddListener(delegate { Zoom(); });
    }

    public void CreateBase()
    {
        GameObject myBuilding = Instantiate(buildingBase);
        myBuilding.transform.position = Vector3.zero;
    }

    public void SetMovingX()
    {
        cameraPivot.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        GameData.gameData.EditMode = GameData.EditModes.MOVING_BUILDING_X;
        textHandler.setModeText("Moving Building X");
    }

    public void SetMovingZ()
    {
        cameraPivot.transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 0));
        GameData.gameData.EditMode = GameData.EditModes.MOVING_BUILDING_Z;
        textHandler.setModeText("Moving Building Z");
    }

    public void SetLiftingMode()
    {
        GameData.gameData.EditMode = GameData.EditModes.LIFT_BUILDING;
        textHandler.setModeText("Moving Building Y");
    }

    public void SetModifyLength()
    {
        cameraPivot.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        GameData.gameData.EditMode = GameData.EditModes.MODIFY_LENGTH;
        textHandler.setModeText("Modifying Length");
    }

    public void SetModifyWidth()
    {
        cameraPivot.transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 0));
        GameData.gameData.EditMode = GameData.EditModes.MODIFY_WIDTH;
        textHandler.setModeText("Modifying Width");
    }

    public void SetModifyHeight()
    {
        GameData.gameData.EditMode = GameData.EditModes.MODIFY_HEIGHT;
        textHandler.setModeText("Modifying Height");
    }

    public void SetRotateX()
    {
        cameraPivot.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        GameData.gameData.EditMode = GameData.EditModes.ROTATE_X;
        textHandler.setModeText("Rotate Building X");
    }

    public void SetRotateY()
    {
        GameData.gameData.EditMode = GameData.EditModes.ROTATE_Y;
        textHandler.setModeText("Rotate Building Y");
    }

    public void SetRotateZ()
    {
        cameraPivot.transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 0));
        GameData.gameData.EditMode = GameData.EditModes.ROTATE_Z;
        textHandler.setModeText("Rotate Building Z");
    }

    public void SetAssignMaterial()
    {
        GameData.gameData.EditMode = GameData.EditModes.ASSIGN_MATERIAL;
        textHandler.setModeText("Assigning Material");
    }

    public void Zoom()
    {
        Camera.main.fieldOfView = camFOV - (zoomSlider.value - 0.5f) * zoomingScale;
    }

    public void Copy()
    {
        if (GameData.gameData.SelectedObject == null) return;
        var parent = GameData.gameData.SelectedObject.transform.parent;
        var copyObject = Instantiate(parent);
        copyObject.transform.localPosition = new Vector3(
            parent.localPosition.x + 0.25f + parent.localScale.x,
            parent.localPosition.y,
            parent.localPosition.z);

        copyObject.transform.localScale = GameData.gameData.SelectedObject.transform.parent.localScale;
    }

    public void Deselect()
    {
        if (GameData.gameData.SelectedObject == null) return;
        GameData.gameData.SelectedObject.GetComponent<ClickTester>().UndoOutline();
        GameData.gameData.SelectedObject = null;
        GameData.gameData.EditMode = GameData.EditModes.IDLE;
        textHandler.setModeText("Select Object");
        UndoHandler.undoHandler.SetActiveButtons(false);
    }

    public void Delete()
    {
        if (GameData.gameData.SelectedObject == null) return;
        UndoHandler.AddStateToStack(GameData.gameData.SelectedObject);
        GameData.gameData.SelectedObject.transform.parent.gameObject.SetActive(false);
        GameData.gameData.EditMode = GameData.EditModes.IDLE;
        textHandler.setModeText("Select Object");
        GameData.gameData.SelectedObject = null;
        UndoHandler.undoHandler.SetActiveButtons(false);
    }

    public void Undo()
    {
        if (UndoHandler.StateQueue.Count == 0) return;
        UndoHandler.UndoState();
    }

    public void AssignWoodTexture()
    {
        if (GameData.gameData.SelectedObject == null) return;
        UndoHandler.AddStateToStack(GameData.gameData.SelectedObject);
        GameData.gameData.SelectedObject.GetComponent<Renderer>().material.mainTexture = textures.woodTexture;
    }

    public void AssignWoodFloorTexture()
    {
        if (GameData.gameData.SelectedObject == null) return;
        UndoHandler.AddStateToStack(GameData.gameData.SelectedObject);
        GameData.gameData.SelectedObject.GetComponent<Renderer>().material.mainTexture = textures.woodFloorTexture;
    }

    public void AssignBrickTexture()
    {
        if (GameData.gameData.SelectedObject == null) return;
        UndoHandler.AddStateToStack(GameData.gameData.SelectedObject);
        GameData.gameData.SelectedObject.GetComponent<Renderer>().material.mainTexture = textures.brickTexture;
    }

    public void AssignConcreteTexture()
    {
        if (GameData.gameData.SelectedObject == null) return;
        UndoHandler.AddStateToStack(GameData.gameData.SelectedObject);
        GameData.gameData.SelectedObject.GetComponent<Renderer>().material.mainTexture = textures.concreteTexture;
    }
}
