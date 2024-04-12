using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ChangeMissionButton_TB : S_Button_TBMA
{
    [Space]
    [SerializeField] GameObject changeTo;

    [SerializeField] List<GameObject> otherTabs;

    public override void OnClick()
    {
        base.OnClick();

        changeTo.SetActive(true);

        for (int i = 0; i < otherTabs.Count; i++)
        {
            otherTabs[i].SetActive(false);
        }
    }
}
