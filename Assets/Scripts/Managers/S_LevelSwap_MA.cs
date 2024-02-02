using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_LevelSwap_MA : MonoBehaviour
{
    [SerializeField] private string nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
                SceneManager.LoadScene(nextLevel);
        }


        //SceneManager.LoadScene("Marie_NextLevel");
    }
}
