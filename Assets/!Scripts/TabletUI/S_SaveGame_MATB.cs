using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class S_SaveGame_MATB : S_Button_TBMA
{
    [SerializeField] S_Player_TB player;
    public override void OnClickEnter(ActionBasedController controller)
    {
        base.OnClickEnter(controller);
        S_SaveSystem_TB.Save(player);
        Debug.Log("saved!");
    }
}
