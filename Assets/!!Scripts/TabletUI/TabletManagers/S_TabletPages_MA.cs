using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_TabletPages_MA : MonoBehaviour
{
    private GameObject player;
    PlayerInput PlayerInput;

    public List<GameObject> tabletPages = new List<GameObject>();

    private int currentPage = 1;
    private float leftOrRight
    {
        get { return PlayerInput.actions["TabletPages"].ReadValue<Vector2>().x; }
    }
    private float waitTime = 0;
    private int firstPage;

    [SerializeField] private GameObject triggerCall;

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<S_Movement_TB>().gameObject;
        PlayerInput = player.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {


        waitTime += Time.deltaTime;
        if (GetComponent<S_Ipad_MA>().isActive)
        {
            if (waitTime >= 0.2f)
            {
                if (leftOrRight > .6f) //counts up
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
                if (leftOrRight < -.6f) //counts down
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
