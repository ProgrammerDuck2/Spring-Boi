using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class S_Restart_MA : S_Button_TBMA
{
    public override void OnClickEnter(ActionBasedController controller)
    {
        base.OnClickEnter(controller);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GetComponent<Image>().color = GetComponent<Button>().colors.pressedColor;
    }

    public override void OnClickExit()
    {
        base.OnClickExit();
        throw new System.NotImplementedException();
    }

    public override void OnHoverEnter(ActionBasedController controller)
    {
        base.OnHoverEnter(controller);
        GetComponent<Image>().color = GetComponent<Button>().colors.highlightedColor;
    }

    public override void OnHoverExit()
    {
        base.OnHoverExit();
        throw new System.NotImplementedException();
    }
}
