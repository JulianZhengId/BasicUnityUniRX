using System.Collections.Generic;
using UnityEngine;

public class UndoHandler : MonoBehaviour
{
    public static UndoHandler undoHandler;
    private static Stack<ObjectState> stateStack;
    [SerializeField] private GameObject[] objectButtons;

    public struct ObjectState
    {
        public Transform parent;
        public Transform child;
        public Vector3 localPosition;
        public Quaternion localRotation;
        public Vector3 localScale;

        public bool active;
        public Texture texture;

        public ObjectState(GameObject obj)
        {
            child = obj.transform;
            parent = child.parent;
            localPosition = parent.localPosition;
            localRotation = parent.localRotation;
            localScale = parent.localScale;
            active = parent.gameObject.activeSelf;
            texture = child.GetComponent<Renderer>().material.mainTexture;
        }

        public override string ToString()
        {
            var str = "Child : " + child.ToString() + "\n";
            str += "Parent : " + parent.ToString() + "\n";
            str += "Position : " + localPosition + "\n";
            str += "Rotation : " + localRotation + "\n";
            str += "Scale : " + localScale + "\n";
            str += "Active : " + active + "\n";
            return str;
        }
    }

    private void Awake()
    {
        Singleton();
        stateStack = new Stack<ObjectState>();
    }

    void Singleton()
    {
        if (UndoHandler.undoHandler == null)
        {
            UndoHandler.undoHandler = this;
        }
        else
        {
            if (UndoHandler.undoHandler != this)
            {
                Destroy(UndoHandler.undoHandler.gameObject);
                UndoHandler.undoHandler = this;
            }
        }
    }

    public static void AddStateToStack(GameObject obj)
    {
        var state = new ObjectState(obj);
        stateStack.Push(state);
    }

    public static void UndoState()
    {
        var state = stateStack.Pop();
       
        state.parent.localPosition = state.localPosition;
        state.parent.localRotation = state.localRotation;
        state.parent.localScale = state.localScale;

        state.parent.gameObject.SetActive(state.active);
        state.child.gameObject.GetComponent<Renderer>().material.mainTexture = state.texture;

        var selectedObj = GameData.gameData.SelectedObject;
        if (selectedObj != null)
        {
            selectedObj.GetComponent<ClickTester>().UndoOutline();
        }
        GameData.gameData.SelectedObject = state.child.gameObject;
        GameData.gameData.SelectedObject.GetComponent<ClickTester>().ApplyOutline();
    }

    public static Stack<ObjectState> StateQueue {
        get { return stateStack; }
    }

    public void SetActiveButtons(bool b)
    {
        foreach (GameObject obj in objectButtons)
        {
            obj.SetActive(b);
        }
    }

}
