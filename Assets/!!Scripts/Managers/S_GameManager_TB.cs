using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class S_GameManager_TB : MonoBehaviour
{
    public GameObject player;
    public GameObject vrManager;
    public GameObject xrInteractionManager;

    GameObject currentPlayer;

    [SerializeField] bool load;

    [SerializeField] S_QuestObject_TB[] allAvailableQuests;
    public int level
    {
        get { return SceneManager.GetActiveScene().buildIndex; }
    }

    void Awake()
    {
        name += " " + SceneManager.GetActiveScene().name;
        DontDestroyOnLoad(this);

        Cursor.lockState = CursorLockMode.Locked;

        S_GameManager_TB[] managers = FindObjectsByType<S_GameManager_TB>(FindObjectsSortMode.InstanceID);

        if (managers.Length >= 2)
        {
            for (int i = 0; i < managers.Length; i++)
            {
                if (managers[i] == this)
                {
                    Debug.LogWarning("Deleted " + managers[i]);
                    Destroy(managers[i].gameObject);
                }
            }
        }

        if(!FindFirstObjectByType<S_VrManager_TB>())
        {
            Instantiate(vrManager);
        }
        if (!FindFirstObjectByType<XRInteractionManager>())
        {
            Instantiate(xrInteractionManager);
        }
        if (!FindFirstObjectByType<S_Player_TB>())
        {
            Debug.LogError("No player in Scene");
        } else
        {
            currentPlayer = FindFirstObjectByType<S_Player_TB>().gameObject;
        }
    }
    private void Start()
    {
        S_Quests_TB.activeQuests.Clear();
        S_Quests_TB.completedQuests.Clear();

        if (load)
            Load();
        //else
        //S_Quests_TB.activeQuests.Add(allAvailableQuests[0]);

        Save();
    }
    void Update()
    {
        if (Debug.isDebugBuild)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    #region Save/Load
    public void Load()
    {
        S_ProgressData_TB data = S_SaveSystem_TB.Load();

        if (data == null) return;

        //Loads Level
        if (data.level != level)
            SceneManager.LoadScene(data.level);

        //Loads Position
        currentPlayer.transform.position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);

        //Loads Current Quests

        for (int i = 0; i < data.currentQuests.Length; i++)
        {
            for (int j = 0; j < allAvailableQuests.Length; j++)
            {
                if (data.currentQuests[i] == allAvailableQuests[j].ID)
                {
                    print("hi");
                    S_Quests_TB.activeQuests.Add(allAvailableQuests[j]);
                    print(allAvailableQuests[j]);
                }
            }
        }

        //Loads Current Quests
        S_Quests_TB.completedQuests.Clear();

        for (int i = 0; i < data.completedQuests.Length; i++)
        {
            for (int j = 0; j < allAvailableQuests.Length; j++)
            {
                if (data.completedQuests[i] == allAvailableQuests[j].ID)
                {
                    print("hi");
                    S_Quests_TB.completedQuests.Add(allAvailableQuests[j]);
                    print(allAvailableQuests[j]);
                }
            }
        }
    }
    public void Save()
    {
        S_SaveSystem_TB.Save(currentPlayer.GetComponent<S_Player_TB>(), this);
    }
    #endregion
}
