using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class S_SaveGame_MATB : S_Button_TBMA
{
    S_GameManager_TB gameManager;
    private void Start()
    {
        gameManager = FindFirstObjectByType<S_GameManager_TB>();
    }
    public override void OnClickEnter(ActionBasedController controller)
    {
        base.OnClickEnter(controller);
        gameManager.Save();
        Debug.Log("saved!");
    }
}
