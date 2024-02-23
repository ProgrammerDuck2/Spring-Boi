using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Ipad_MA : MonoBehaviour
{
    private bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int childCount = transform.childCount;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isActive = !isActive;

            for (int i = 0; i < childCount; i++)
            {
                if (isActive == true)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        
    }
}
