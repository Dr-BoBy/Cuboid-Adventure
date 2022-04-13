using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pause_btn_quit : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Start(){
        text = GetComponent<TextMeshProUGUI>();
    }

    public void quitGame(){
        Application.Quit();
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
