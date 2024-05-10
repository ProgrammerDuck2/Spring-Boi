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
}
