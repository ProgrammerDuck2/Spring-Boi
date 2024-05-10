using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_RespawnButton_TB : S_Button_TBMA
{
    S_Respawn_MA respawn;
    S_Player_TB player;
    private void Start()
    {
        player = FindFirstObjectByType<S_Player_TB>();
        respawn = player.GetComponent<S_Respawn_MA>();
    }
    public override void OnClick()
    {
        base.OnClick();

        player.transform.position = respawn.respawnPoint;
    }
}
