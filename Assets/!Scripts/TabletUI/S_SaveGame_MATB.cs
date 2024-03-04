using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_SaveGame_MATB : MonoBehaviour, S_Interactable_TBMA
{
    //Correctly get and set colors
    [SerializeField] Color _ButtonColor;
    public Color ButtonColor
    {
        get { return _ButtonColor; }
        set { _ButtonColor = value; }
    }
    [SerializeField] Color _HighlightColor;
    public Color HighlightColor
    {
        get { return _HighlightColor; }
        set { _HighlightColor = value; }
    }
    [SerializeField] Color _PressedColor;
    public Color PressedColor
    {
        get { return _PressedColor; }
        set { _PressedColor = value; }
    }

    public Image ButtonImage
    {
        get { return GetComponent<Image>(); }
    }

    public void OnClickEnter()
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
        print(ButtonImage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickExit()
    {
        throw new System.NotImplementedException();
    }
}
