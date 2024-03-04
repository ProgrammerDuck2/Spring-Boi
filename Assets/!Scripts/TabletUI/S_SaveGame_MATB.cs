using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_SaveGame_MATB : MonoBehaviour, S_ButtonInterface_TBMA
{
    public void OnClick()
    {
        Debug.Log("saved. no bird accessable");
        GetComponent<Image>().color = GetComponent<Button>().colors.pressedColor;
    }

    public void OnHover()
    {
        Debug.Log("hover Save");
        GetComponent<Image>().color = GetComponent<Button>().colors.highlightedColor;
    }
}
