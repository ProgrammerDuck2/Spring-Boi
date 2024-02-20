using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class S_UIText_MA : MonoBehaviour
{
    public TMP_Text text;
    [SerializeField] private List<string> uitext = new List<string>();
    private int currentText = 0;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    //Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentText == uitext.Count)
            {
                text.gameObject.SetActive(false);
                return;
            }

            //StartCoroutine(UIText());         //- if timed
            text.text = uitext[currentText];    //if player clicks trough
            currentText++;
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
