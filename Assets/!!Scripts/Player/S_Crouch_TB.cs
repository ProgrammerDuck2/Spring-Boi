using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Crouch_TB : S_Player_TB
{
    [ShowIf("DebugMode")]
    public bool isCrouching;

    float crouchLimit;

    private IEnumerator Start()
    {
        while (IRLPosition.y == 0)
        {
            yield return new WaitForEndOfFrame();
        }
        crouchLimit = IRLPosition.y - .3f;
    }
    void Update()
    {
        if (!S_Settings_TB.IsVRConnected) return;

        playerArt.transform.localScale = new Vector3(1, IRLPosition.y / 2 + .75f, 1);

        if (IRLPosition.y < crouchLimit)
        {
            isCrouching = true;
        } else
        {
            isCrouching = false;
        }
    }
}
