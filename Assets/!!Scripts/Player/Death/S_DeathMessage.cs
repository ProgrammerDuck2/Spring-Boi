using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_DeathMessage : MonoBehaviour
{
    [SerializeField] S_DeathArea_TB deathArea;
    TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = deathArea.deathMessage;
    }
}
