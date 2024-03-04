using UnityEngine;
using UnityEngine.UI;

public class S_Button_TBMA : MonoBehaviour
{
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
    public virtual void OnClickEnter()
    {

    }
    public virtual void OnClick()
    {

    }
    public virtual void OnClickExit()
    {

    }
    public virtual void OnHoverEnter()
    {

    }
    public virtual void OnHover()
    {

    }

    public virtual void OnHoverExit()
    {

    }
}
