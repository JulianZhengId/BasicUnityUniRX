using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using UnityEngine.UI;

public class ClickTester : MonoBehaviour
{
    public IObservable<EventArgs> clickTrigger;
    private Renderer myRenderer;
    private TextHandler textHandler;

    private void Awake()
    {
        textHandler = FindObjectOfType<TextHandler>();
        myRenderer = GetComponent<Renderer>();
        clickTrigger = gameObject.AddComponent<ObservablePointerClickTrigger>()
            .OnPointerClickAsObservable()
            .Select(e => EventArgs.Empty);
    }

    private void Start()
    {
        myRenderer.material.SetFloat("_OutlineWidth", 1);
        clickTrigger.Subscribe(clickOutput).AddTo(this);
    }

    public void clickOutput(EventArgs e)
    {
        //Check GameData
        if (GameData.gameData == null) return;

        //Check Selected Object
        if (GameData.gameData.SelectedObject == this.gameObject) return;

        //Undo Outline
        if (GameData.gameData.SelectedObject != null)
        {
            GameData.gameData.SelectedObject.GetComponent<ClickTester>().UndoOutline();
        }

        //Set Text
        SetBuildingAttributesText();

        //Apply Outline
        GameData.gameData.SelectedObject = this.gameObject;
        ApplyOutline();
    }

    private void SetBuildingAttributesText()
    {
        var parent = this.gameObject.transform.parent;
        textHandler.setBuildingLengthText(this.transform.localScale.x.ToString());
        textHandler.setBuildingWidthText(this.transform.localScale.z.ToString());
        textHandler.setBuildingHeightText(parent.transform.localScale.y.ToString());
        textHandler.setPositionText(parent.transform.localPosition);
    }

    public void UndoOutline()
    {
        myRenderer.material.SetFloat("_OutlineWidth", 1);
    }

    private void ApplyOutline()
    {
        myRenderer.material.SetFloat("_OutlineWidth", 1.05f);
        myRenderer.material.SetColor("_OutlineColor", new Color(1f, 0.64f, 0f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collide");
    }
}
