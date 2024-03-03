using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_QuitGame_MA : MonoBehaviour, S_ButtonInterface_TBMA
{
    public void OnClick()
    {
        Application.Quit();
        GetComponent<Image>().color = GetComponent<Button>().colors.pressedColor;
    }

    public void OnHover()
    {
        GetComponent<Image>().color = GetComponent<Button>().colors.highlightedColor;
    }
}
