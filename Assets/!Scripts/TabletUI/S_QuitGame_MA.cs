using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_QuitGame_MA : MonoBehaviour, S_Interactable_TBMA
{
    public Color ButtonColor { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public Color HighlightColor { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public Color PressedColor { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public Image ButtonImage => throw new System.NotImplementedException();

    public void OnClickEnter()
    {
        Application.Quit();
        GetComponent<Image>().color = GetComponent<Button>().colors.pressedColor;
    }

    public void OnClickExit()
    {
        throw new System.NotImplementedException();
    }

    public void OnHover()
    {
        GetComponent<Image>().color = GetComponent<Button>().colors.highlightedColor;
    }

    public void OnHoverExit()
    {
        throw new System.NotImplementedException();
    }
}
