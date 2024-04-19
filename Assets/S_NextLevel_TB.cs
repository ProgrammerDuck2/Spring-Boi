using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_NextLevel_TB : MonoBehaviour
{
    [Scene]
    [SerializeField] string nextLevel;
    private void OnCollisionEnter(Collision other)
    {
        //Invoke(nameof(NextLevel), 1);
    }
    void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
