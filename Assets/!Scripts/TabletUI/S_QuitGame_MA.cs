using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class S_QuitGame_MA : S_Button_TBMA
{
    public override void OnClickEnter(ActionBasedController controller)
    {
        base.OnClickEnter(controller);
        Application.Quit();
    }

    public override void OnHoverEnter(ActionBasedController controller)
    {
        base.OnHoverEnter(controller);
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
