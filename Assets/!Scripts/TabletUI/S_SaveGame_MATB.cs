using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_SaveGame_MATB : MonoBehaviour, S_ButtonInterface_TBMA
{
    public Color ButtonColor
    {
        get { return ButtonColor; }
        set { ButtonColor = value; }
    }
    public Color HighlightColor
    {
        get { return HighlightColor; }
        set { HighlightColor = value; }
    }
    public Color PressedColor
    {
        get { return PressedColor; }
        set { PressedColor = value; }
    }

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

    public void OnHoverExit()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
