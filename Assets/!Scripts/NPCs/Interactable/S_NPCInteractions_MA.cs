
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_NPCInteractions_MA : S_NPC_TB
{
    Transform playerTransform
    {
        get { return player.transform; }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = playerTransform.position - NPCText.transform.position;
        Quaternion rotation = Quaternion.LookRotation(-direction);
        NPCText.transform.rotation = rotation;
    }

    //moved a lot of code over to S_NPC_TB as to easily and convinently check whether the npc has been talked to :) ps: none of the code has been changed, just moved

    public override void StartSpeech(InputAction.CallbackContext context)
    {
        base.StartSpeech(context);
    }

    public override IEnumerator Speech()
    {
        return base.Speech();
    }
}