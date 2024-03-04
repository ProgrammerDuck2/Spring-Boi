using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class S_SaveGame_MATB : S_Button_TBMA
{

    public override void OnClickEnter()
    {
        base.OnClickEnter();
        Debug.Log("saved. no bird accessable");
    }

    public override void OnHoverEnter()
    {
        base.OnHover();
        Debug.Log("hover Save");
    }

    public override void OnHoverExit()
    {
        base.OnHoverExit();
        throw new System.NotImplementedException();
    }
    public override void OnClickExit()
    {
        base.OnClickExit();
        throw new System.NotImplementedException();
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
