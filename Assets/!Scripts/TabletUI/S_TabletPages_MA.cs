using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_TabletPages_MA : MonoBehaviour
{
    private GameObject player;
    PlayerInput PlayerInput;

    public List<GameObject> tabletPages = new List<GameObject>();

    private int currentPage = 1;
    private float leftOrRight;
    private float waitTime = 0;
    private int firstPage = 1;

    [SerializeField] private GameObject triggerCall;

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<S_Movement_TB>().gameObject;
        PlayerInput = player.GetComponent<PlayerInput>();
        leftOrRight = PlayerInput.actions["TabletPages"].ReadValue<Vector2>().x;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentPage);
        if (triggerCall.GetComponent<S_CallTrigger_MA>().tabletTrigger)
        {
            firstPage = 0;
        }

        waitTime += Time.deltaTime;
        if (GetComponent<S_Ipad_MA>().isActive)
        {

            leftOrRight = PlayerInput.actions["TabletPages"].ReadValue<Vector2>().x;
            if (waitTime >= 0.2f)
            {
                if (leftOrRight > 0) //counts up
                {
                    if (currentPage == firstPage)
                    {
                        currentPage = tabletPages.Count - 1;
                    }
                    else
                    {
                        currentPage--;
                    }
                }
                if (leftOrRight < 0) //counts down
                {
                    if (currentPage >= tabletPages.Count - 1)
                    {
                        currentPage = firstPage;
                    }
                    else
                    {
                        currentPage++;
                    }
                }
                waitTime = 0;
            }
            for (int i = 0; i < tabletPages.Count; i++)
            {
                if(i != currentPage)
                    tabletPages[i].gameObject.SetActive(false);
            }
            tabletPages[currentPage].gameObject.SetActive(true);
        }
        //Debug.Log(triggerCall);
    }
}
