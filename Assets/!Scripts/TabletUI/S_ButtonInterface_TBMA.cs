using UnityEngine;

public interface S_ButtonInterface_TBMA
{
    public Color ButtonColor { get; set; }
    public Color HighlightColor { get; set; }
    public Color PressedColor { get; set; }
    public void OnClick();

    public void OnHover();

    public void OnHoverExit();
}
