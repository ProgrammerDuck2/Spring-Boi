using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(BoxCollider))]
public class S_Button_TBMA : MonoBehaviour
{
    S_HapticFeedback_TB hapticFeedback
    {
        get { return FindFirstObjectByType<S_HapticFeedback_TB>(); }
    }

    [SerializeField] Color _ButtonColor = Color.white;
    public Color ButtonColor
    {
        get { return _ButtonColor; }
        set { _ButtonColor = value; }
    }
    [SerializeField] Color _HighlightColor = Color.white;
    public Color HighlightColor
    {
        get { return _HighlightColor; }
        set { _HighlightColor = value; }
    }
    [SerializeField] Color _PressedColor = Color.white;
    public Color PressedColor
    {
        get { return _PressedColor; }
        set { _PressedColor = value; }
    }
    public Image ButtonImage
    {
        get { return GetComponent<Image>(); }
    }
    public virtual void OnClickEnter(ActionBasedController controller)
    {
        hapticFeedback.TriggerHaptic(.3f, .1f, controller);
    }
    public virtual void OnClick()
    {
        ButtonImage.color = PressedColor;
    }
    public virtual void OnClickExit()
    {
        ButtonImage.color = ButtonColor;
    }
    public virtual void OnHoverEnter(ActionBasedController controller)
    {
        hapticFeedback.TriggerHaptic(.3f, .1f, controller);
    }
    public virtual void OnHover()
    {
        ButtonImage.color = HighlightColor;
    }

    public virtual void OnHoverExit()
    {
        ButtonImage.color = ButtonColor;
    }
}