using UnityEngine;
using UnityEngine.UI;

public interface S_ButtonInterface_TBMA
{
    public Image ButtonImage { get; }
    public Color ButtonColor { get; set; }
    public Color HighlightColor { get; set; }
    public Color PressedColor { get; set; }
    public void OnClick();

    public void OnHover();

    public void OnHoverExit();
}
