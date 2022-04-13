using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class pause_btn_menu : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Start(){
        text = GetComponent<TextMeshProUGUI>();
    }

    public void loadMenu(){
        Time.timeScale=1f;
        SceneManager.LoadScene("Menu");
    }

    public void mouseEnter(){
        text.fontSize=28;
        text.color=Color.yellow;
    }

    public void mouseExit(){
        
        text.fontSize=26;
        text.color=Color.white;
    }
}
