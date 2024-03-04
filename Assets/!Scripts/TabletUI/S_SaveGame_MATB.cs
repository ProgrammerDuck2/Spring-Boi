using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class S_SaveGame_MATB : S_Button_TBMA
{

    public override void OnClickEnter(ActionBasedController controller)
    {
        base.OnClickEnter(controller);
        Debug.Log("saved. no bird accessable");
    }

    public override void OnHoverEnter(ActionBasedController controller)
    {
        base.OnHoverEnter(controller);
        Debug.Log("hover Save");
    }

    public override void OnHoverExit()
    {
        base.OnHoverExit();
    }
    public override void OnClickExit()
    {
        base.OnClickExit();
    }

    // Start is called before the first frame update
    void Start()
    {
        print(ButtonImage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
