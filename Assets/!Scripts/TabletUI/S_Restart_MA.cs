using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S_Restart_MA : MonoBehaviour, S_ButtonInterface_TBMA
{
    public void OnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GetComponent<Image>().color = GetComponent<Button>().colors.pressedColor;
    }

    public void OnHover()
    {
        GetComponent<Image>().color = GetComponent<Button>().colors.highlightedColor;
    }
}
