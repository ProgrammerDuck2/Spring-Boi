using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_Ipad_MA : MonoBehaviour
{
    public bool isActive = false;
    private GameObject player;
    PlayerInput PlayerInput;
    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<S_Movement_TB>().gameObject;
        PlayerInput = player.GetComponent<PlayerInput>();
        PlayerInput.actions["OpenTablet"].started += OpenTablet;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OpenTablet(InputAction.CallbackContext context)
    {
        int childCount = transform.childCount;
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
