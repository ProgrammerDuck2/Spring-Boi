using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class S_Restart_MA : S_Button_TBMA
{
    S_GameManager_TB gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<S_GameManager_TB>();
    }

    public override void OnClickEnter(ActionBasedController controller)
    {
        base.OnClickEnter(controller);
        gameManager.Load();
        
    }
}
