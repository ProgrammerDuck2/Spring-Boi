using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_NPCInteractions_MA : MonoBehaviour
{
    [SerializeField] private List<string> speech = new List<string>();
    //private int currentSpeech = 0;

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
            if (Input.GetKeyDown(KeyCode.E))
            {
                    StartCoroutine(Speech());
            }
        }
    }

    IEnumerator Speech()
    {
        for (int i = 0; i < speech.Count; i++) 
        {
            print(speech[i]);
            //currentSpeech++;
            yield return new WaitForSeconds(1);
        }
    }
}