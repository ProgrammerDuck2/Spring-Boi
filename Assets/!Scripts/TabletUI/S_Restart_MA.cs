using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S_Restart_MA : S_Button_TBMA
{
    public override void OnClickEnter()
    {
        base.OnClickEnter();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GetComponent<Image>().color = GetComponent<Button>().colors.pressedColor;
    }

    public override void OnClickExit()
    {
        base.OnClickExit();
        throw new System.NotImplementedException();
    }

    public override void OnHoverEnter()
    {
        base.OnHover();
        GetComponent<Image>().color = GetComponent<Button>().colors.highlightedColor;
    }

    public override void OnHoverExit()
    {
        base.OnHoverExit();
        throw new System.NotImplementedException();
    }
}
