using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class btn_next_lvl : MonoBehaviour
{
    public string nextLvlName;
    private TextMeshProUGUI text;

    void Start(){
        text = GetComponent<TextMeshProUGUI>();
    }

    public void loadNextLvl(){
        Time.timeScale=1f;
        SceneManager.LoadScene(nextLvlName);
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
