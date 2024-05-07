using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_LevelSwap_MA : MonoBehaviour
{
    [Scene]
    public string NewScene;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
                SceneManager.LoadScene(NewScene);
        }
    }
}
