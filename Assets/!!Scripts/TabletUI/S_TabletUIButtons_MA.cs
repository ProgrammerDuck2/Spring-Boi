using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_TabletUIButtons_MA : MonoBehaviour
{
    [SerializeField] private List<GameObject> buttons = new List<GameObject>();
    private int currentButton;
    private int firstButton = 1;

    private GameObject player;
    PlayerInput PlayerInput;

    private float upOrDown;
    private float waitTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<S_Movement_TB>().gameObject;
        PlayerInput = player.GetComponent<PlayerInput>();
        PlayerInput.actions["TabletInteractions"].started += InteractButton;
        upOrDown = PlayerInput.actions["TabletPages"].ReadValue<Vector2>().y;
    }

    // Update is called once per frame
    void Update()
    {
        upOrDown = PlayerInput.actions["TabletPages"].ReadValue<Vector2>().y;

        waitTime += Time.deltaTime;
        if (waitTime >= 0.2f)
        {
            if (upOrDown > 0) //counts up
            {
                if (currentButton == firstButton)
                {
                    currentButton = buttons.Count - 1;
                }
                else
                {
                    currentButton--;
                }
            }
            if (upOrDown < 0) //counts down
            {
                if (currentButton >= buttons.Count - 1)
                {
                    currentButton = firstButton;
                }
                else
                {
                    currentButton++;
                }
            }
            waitTime = 0;
            //Debug.Log(currentButton);
        }
    }

    void InteractButton(InputAction.CallbackContext context)
    {


    }
}
