using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    

    //ANIMATION
    public Animator anim;
    public bool isWalking;
    public bool isDamaged;

    //SOUND
    public PlayerController playerController;

    
    void Start()
    {
        anim.SetBool("isWinning",false);
    }

    void Update(){
        if(!PlayerController.levelIsFinished && !pauseMenu.gameIsPaused){
            //DEPLACEMENT
            if (Input.GetKey (KeyCode.Q)) {
                isWalking=true;
                if(!GetComponent<AudioSource>().isPlaying && playerController.canJump){
                    GetComponent<AudioSource>().Play();
                }
            }
            if (Input.GetKeyUp (KeyCode.Q)) {
                isWalking=false;
            }
            if (Input.GetKey (KeyCode.D)) {
                isWalking=true;
                if(!GetComponent<AudioSource>().isPlaying && playerController.canJump){
                    GetComponent<AudioSource>().Play();
                }
            }
            if (Input.GetKeyUp (KeyCode.D)) {
                isWalking=false;
            }
            if(isWalking){
                anim.SetBool("isWalking",true);
            }
            if(!isWalking){
                anim.SetBool("isWalking",false);
            }
            if(isDamaged){
                anim.SetBool("isDamaged",true);
            }
            if(!isDamaged){
                anim.SetBool("isDamaged",false);
            }
        }
    }
}
