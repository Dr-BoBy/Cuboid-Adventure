using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class lose_btn_restart : MonoBehaviour
{
    private TextMeshProUGUI text;
    public string level;

    void Start(){
        text = GetComponent<TextMeshProUGUI>();
    }

    public void restart(){
        Time.timeScale=1f;
        SceneManager.LoadScene(level);
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
