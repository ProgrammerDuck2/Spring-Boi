using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public void OnHover()
    {
        Debug.Log("hover Save");
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
