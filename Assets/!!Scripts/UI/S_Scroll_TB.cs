using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class S_Scroll_TB : S_VRUI_TB
{
    Scrollbar scrollbar;
    [SerializeField] InputActionProperty scroll;
    float scrollValue
    {
        get { return scroll.action.ReadValue<Vector2>().y; }
    }
    private void Start()
    {
        scrollbar = transform.GetChild(1).GetComponent<Scrollbar>();
    }
    void Update()
    {
        scrollbar.value += scrollValue / 10;
    }
}
