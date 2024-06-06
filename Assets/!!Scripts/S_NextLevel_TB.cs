using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_NextLevel_TB : MonoBehaviour
{
    [Scene]
    [SerializeField] string nextLevel;

    S_GameManager_TB gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<S_GameManager_TB>();
    }
    private void OnCollisionEnter(Collision other)
    {
        Invoke(nameof(NextLevel), 7);
    }
    void NextLevel()
    {
        gameManager.Save();
        //SceneManager.LoadScene(nextLevel);
    }
}
