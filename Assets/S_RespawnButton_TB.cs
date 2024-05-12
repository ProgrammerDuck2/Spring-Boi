using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_RespawnButton_TB : S_Button_TBMA
{
    S_Player_TB player;


    private void Start()
    {
        player = FindFirstObjectByType<S_Player_TB>();
    }
    public override void OnClick()
    {
        base.OnClick();

        S_ProgressData_TB data = S_SaveSystem_TB.Load();
        player.transform.position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
    }
}
