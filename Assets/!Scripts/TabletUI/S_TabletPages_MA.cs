using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_TabletPages_MA : MonoBehaviour
{
    private GameObject player;
    PlayerInput PlayerInput;

    [SerializeField] private List<GameObject> tabletPages = new List<GameObject>();
    private int currentPage = 0;
    private float XX;
    private float waitTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<S_Movement_TB>().gameObject;
        PlayerInput = player.GetComponent<PlayerInput>();
        XX = PlayerInput.actions["TabletPages"].ReadValue<Vector2>().x;
    }

    // Update is called once per frame
    void Update()
    {
        waitTime += Time.deltaTime;
        if (GetComponent<S_Ipad_MA>().isActive) 
        {
        XX = PlayerInput.actions["TabletPages"].ReadValue<Vector2>().x;
        if (waitTime >= 0.2f)
        {
            if (XX > 0)
            {
                if (currentPage == 0)
                {
                    currentPage = tabletPages.Count - 1;
                }
                else
                {
                    currentPage--;
                }
            }
            if (XX < 0)
            {
                if (currentPage >= tabletPages.Count - 1)
                {
                    currentPage = 0;
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
            tabletPages[i].gameObject.SetActive(false);
        }
        tabletPages[currentPage].gameObject.SetActive(true);
        }
    }
}
