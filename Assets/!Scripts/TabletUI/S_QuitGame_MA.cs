using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_QuitGame_MA : S_Button_TBMA
{
    public override void OnClickEnter()
    {
        base.OnClickEnter();
        Application.Quit();
    }

    public override void OnHoverEnter()
    {
        base.OnHover();
        throw new System.NotImplementedException();
    }

    public override void OnHoverExit()
    {
        base.OnHoverExit();
        throw new System.NotImplementedException();
    }
    public override void OnClickExit()
    {
        base.OnClickExit();
        throw new System.NotImplementedException();
    }
}
