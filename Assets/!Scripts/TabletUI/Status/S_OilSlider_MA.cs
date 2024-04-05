using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_OilSlider_MA : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        S_Stats_MA.oilCollected = 0;
        slider.maxValue = S_Stats_MA.maxOil;
        slider.value = S_Stats_MA.oilCollected;
    }

    private void Update()
    {
        slider.value = S_Stats_MA.oilCollected;
    }
}
