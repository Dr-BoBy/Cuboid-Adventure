using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public PlayerController playerController;
    private Vector3 offset;
    public float speed = 0.04f;
    private Quaternion rotaCamGauche = Quaternion.Euler(0,90,0);
    private Quaternion rotaCamDroite = Quaternion.Euler(0,-90,0);
    private Quaternion rotaPlayerGauche = Quaternion.Euler(0,90,0);
    private Quaternion rotaPlayerDroite = Quaternion.Euler(0,-90,0);
    public Quaternion targetCameraAngle;

    void Start()
    {
        offset = transform.position - player.transform.position;
        targetCameraAngle=transform.rotation;
    }

    void Update(){
        if(!PlayerController.levelIsFinished && !pauseMenu.gameIsPaused){
            if(Input.GetKeyDown(KeyCode.LeftArrow)){
                ChangeCameraTargetAngle(rotaCamGauche);
                ChangePlayerTargetAngle(rotaPlayerGauche);
            }
            if(Input.GetKeyDown(KeyCode.RightArrow)){
                ChangeCameraTargetAngle(rotaCamDroite);
                ChangePlayerTargetAngle(rotaPlayerDroite);
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, targetCameraAngle, speed);
        }
    }
    
    // Update is called once per frame
    void LateUpdate(){
        if(!PlayerController.levelIsFinished && !pauseMenu.gameIsPaused){
            transform.position = player.transform.position + offset;
        }
    }

    void ChangeCameraTargetAngle(Quaternion angle){
        targetCameraAngle*=angle;
        if(angle==rotaCamDroite){
            playerController.orientation-=90;
            if(playerController.orientation<0){
                 playerController.orientation+=360;
            }
        }
        if(angle==rotaCamGauche){
            playerController.orientation+=90;
            if(playerController.orientation>=360){
                 playerController.orientation-=360;
            }
        }
    }
    void ChangePlayerTargetAngle(Quaternion angle){
        playerController.targetPlayerAngle*=angle;
    }
}
