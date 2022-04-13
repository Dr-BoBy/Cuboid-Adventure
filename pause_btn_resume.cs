using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pause_btn_resume : MonoBehaviour
{
    private TextMeshProUGUI text;
    public GameObject pauseMenuUI;

    void Start(){
        text = GetComponent<TextMeshProUGUI>();
    }

   public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        pauseMenu.gameIsPaused = false;
        text.fontSize=26;
        text.color=Color.white;
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
