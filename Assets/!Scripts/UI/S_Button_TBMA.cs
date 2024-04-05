using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(BoxCollider))]
public class S_Button_TBMA : S_VRUI_TB
{
    [SerializeField] Color _ButtonColor = Color.white;
    public Color ButtonColor
    {
        get { return _ButtonColor; }
        set { _ButtonColor = value; }
    }
    [SerializeField] Color _HighlightColor = new Color(0.4f, .7f, 0.6f);
    public Color HighlightColor
    {
        get { return _HighlightColor; }
        set { _HighlightColor = value; }
    }
    [SerializeField] Color _PressedColor = new Color(0.3f, .7f, 0.6f);
    public Color PressedColor
    {
        get { return _PressedColor; }
        set { _PressedColor = value; }
    }
    public Image ButtonImage
    {
        get { return GetComponent<Image>(); }
    }


    public override void OnClickEnter(ActionBasedController controller)
    {
        base.OnClickEnter(controller);
    }
    public override void OnClick()
    {
        base.OnClick();
        ButtonImage.color = PressedColor;
    }
    public override void OnClickExit()
    {
        base.OnClickExit();
        ButtonImage.color = ButtonColor;
    }

    public override void OnHoverEnter(ActionBasedController controller)
    {
        base.OnHoverEnter(controller);
    }
    public override void OnHover()
    {
        base.OnHover();
        ButtonImage.color = HighlightColor;
    }

    public override void OnHoverExit()
    {
        base.OnHoverExit();
        ButtonImage.color = ButtonColor;
    }
}