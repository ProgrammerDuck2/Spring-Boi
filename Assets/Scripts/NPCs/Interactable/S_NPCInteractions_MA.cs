
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_NPCInteractions_MA : MonoBehaviour
{
    public TMP_Text NPCText;
    [SerializeField] private List<string> speech = new List<string>();
    //private int currentSpeech = 0;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        NPCText.transform.LookAt(player.transform);
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
    //transform.LookAt(turningPoints[nextPoint].transform);
    IEnumerator Speech()
    {
        for (int i = 0; i < speech.Count; i++) 
        {
            print(speech[i]);
            NPCText.text = speech[i];
            //currentSpeech++;
            yield return new WaitForSeconds(1);
        }
    }
}