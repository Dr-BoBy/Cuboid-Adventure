using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
public class btn_options : MonoBehaviour
{
    private TextMeshPro text;
    void Start(){
        text = GetComponent<TextMeshPro>();    
    }

    void OnMouseOver(){   
        text.fontSize=12;
        text.color=Color.yellow;
    }

    void OnMouseExit(){
        text.fontSize=10;
        text.color=Color.white;
    }

    void OnMouseUp(){
        SceneManager.LoadScene("Option");
    }
}
