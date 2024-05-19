using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_OilGathered_MA : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        text.text = S_Stats_MA.oilCollected.ToString();
    }
}
