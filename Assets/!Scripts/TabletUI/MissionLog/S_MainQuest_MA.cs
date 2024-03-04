using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_MainQuest_MA : MonoBehaviour
{
    [SerializeField] GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        button.GetComponent<Image>().color = button.GetComponent<Button>().colors.pressedColor;
        Debug.Log("task completed");
    }

}
