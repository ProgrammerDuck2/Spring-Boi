using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class S_UIText_MA : MonoBehaviour
{
    public TextMeshProUGUI text;
    [SerializeField] private List<string> uitext = new List<string>();
    private int currentText = 0;

    // Start is called before the first frame update
    void Start()
    {
        text = FindObjectOfType<TextMeshProUGUI>();
    }

    //Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //StartCoroutine(UIText());         //- if timed
            text.text = uitext[currentText];    //if player clicks trough
            currentText++;
            //if (currentText == uitext.Count)
            {
                //uitext.gameObject.enabled = false;
            }
        }
    }


    //IEnumerator UIText()
    //{
    //    for (int i = 0; i < uitext.Count; i++)
    //    {
    //        text.text = (uitext[i]);
    //        yield return new WaitForSeconds(1);S
    //    }
    //}
}
