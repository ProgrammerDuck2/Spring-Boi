using UnityEngine;
using UnityEngine.UI;

public interface S_Interactable_TBMA
{
    public Image ButtonImage { get; }
    public Color ButtonColor { get; set; }
    public Color HighlightColor { get; set; }
    public Color PressedColor { get; set; }
    public void OnClickEnter();
    public void OnClickExit();

    public void OnHover();

    public void OnHoverExit();
}
