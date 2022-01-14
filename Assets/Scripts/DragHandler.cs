using UniRx;
using UniRx.Triggers;
using UnityEngine;
using System;

public class DragHandler : MonoBehaviour
{
    public IObservable<Vector2> dragTrigger;
    public IObservable<EventArgs> beginDragTrigger;

    [SerializeField] private float scalingFactor = 10f;
    [SerializeField] private float movingFactor = 10f;
    [SerializeField] private float rotatingFactor = 10f;
    [SerializeField] private float minScale = 0.05f;
    [SerializeField] private float maxScale = 150f;
    [SerializeField] private TextHandler textHandler;
    [SerializeField] private GameObject cameraPivot;

    private void Awake()
    {
        dragTrigger = gameObject.AddComponent<ObservableDragTrigger>()
            .OnDragAsObservable()
            .Select(e => e.delta);

        beginDragTrigger = gameObject.AddComponent<ObservableBeginDragTrigger>()
            .OnBeginDragAsObservable()
            .Select(e => EventArgs.Empty);
    }

    private void Start()
    {
        dragTrigger.Subscribe(Drag).AddTo(this);
        beginDragTrigger.Subscribe(BeginDrag).AddTo(this);
    }

    private void Drag(Vector2 v)
    {
        if (GameData.gameData == null) return;
        if (GameData.gameData.SelectedObject == null || GameData.gameData.EditMode == GameData.EditModes.IDLE)
        {
            var angleX = new Vector3(-v.y * rotatingFactor * Time.deltaTime, 0f, 0f);
            var angleY = new Vector3(0f , v.x * rotatingFactor * Time.deltaTime, 0f);
            cameraPivot.transform.Rotate(angleY, Space.World);
            cameraPivot.transform.Rotate(angleX, Space.Self);
            return;
        }

        var selectedObject = GameData.gameData.SelectedObject.transform;
        var parentSelectedObject = selectedObject.parent;
        if (GameData.gameData.EditMode == GameData.EditModes.MOVING_BUILDING_X)
        {
            parentSelectedObject.localPosition += new Vector3(
                v.x * Time.deltaTime * movingFactor,
                0f,
                0f);
        }
        else if (GameData.gameData.EditMode == GameData.EditModes.MOVING_BUILDING_Z)
        {
            parentSelectedObject.localPosition += new Vector3(
                0f,
                0f,
                v.x * Time.deltaTime * movingFactor);
        }
        else if (GameData.gameData.EditMode == GameData.EditModes.LIFT_BUILDING)
        {
            parentSelectedObject.localPosition += new Vector3(
                0f,
                v.y * Time.deltaTime * movingFactor,
                0f);
            parentSelectedObject.localPosition = new Vector3(
                Mathf.Clamp(parentSelectedObject.localPosition.x, -200f, 200f),
                Mathf.Clamp(parentSelectedObject.localPosition.y, 0, 200f),
                Mathf.Clamp(parentSelectedObject.localPosition.z, -200f, 200f));
        }
        else if (GameData.gameData.EditMode == GameData.EditModes.MODIFY_LENGTH)
        {
            parentSelectedObject.localScale += new Vector3(v.x * scalingFactor * Time.deltaTime, 0f, 0f);
        }
        else if (GameData.gameData.EditMode == GameData.EditModes.MODIFY_WIDTH)
        {
            parentSelectedObject.localScale += new Vector3(0f, 0f, v.x * scalingFactor * Time.deltaTime);
        }
        else if (GameData.gameData.EditMode == GameData.EditModes.MODIFY_HEIGHT)
        {
            parentSelectedObject.localScale += new Vector3(0f, v.y * scalingFactor * Time.deltaTime, 0f);
        }
        else if (GameData.gameData.EditMode == GameData.EditModes.ROTATE_X)
        {
            var angle = new Vector3(0f, 0f, -v.x * rotatingFactor * Time.deltaTime);
            parentSelectedObject.transform.Rotate(angle, Space.World);
        }
        else if (GameData.gameData.EditMode == GameData.EditModes.ROTATE_Y)
        {
            var angle = new Vector3(0f, v.x * rotatingFactor * Time.deltaTime, 0f);
            parentSelectedObject.transform.Rotate(angle, Space.World);
        }
        else if (GameData.gameData.EditMode == GameData.EditModes.ROTATE_Z)
        {
            var angle = new Vector3(v.x * rotatingFactor * Time.deltaTime, 0f, 0f);
            parentSelectedObject.transform.Rotate(angle, Space.World);
        }



        //Clamping
        selectedObject.localScale = new Vector3(
            Mathf.Clamp(selectedObject.localScale.x, minScale, maxScale),
            Mathf.Clamp(selectedObject.localScale.y, minScale, maxScale),
            Mathf.Clamp(selectedObject.localScale.z, minScale, maxScale));

        parentSelectedObject.localScale = new Vector3(
            Mathf.Clamp(parentSelectedObject.localScale.x, minScale, maxScale),
            Mathf.Clamp(parentSelectedObject.localScale.y, minScale, maxScale),
            Mathf.Clamp(parentSelectedObject.localScale.z, minScale, maxScale));

        //Set Text
        textHandler.setAttributesText(new Vector3(
            selectedObject.localScale.x,
            parentSelectedObject.localScale.y,
            selectedObject.localScale.z));

        textHandler.setPositionText(parentSelectedObject.localPosition);
    }

    private void BeginDrag(EventArgs e)
    {
        if (GameData.gameData == null) return;
        if (GameData.gameData.SelectedObject == null) return;
        if (GameData.gameData.EditMode == GameData.EditModes.IDLE) return;

        UndoHandler.AddStateToStack(GameData.gameData.SelectedObject);
    }
}
