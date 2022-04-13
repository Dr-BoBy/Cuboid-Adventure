using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{
    //AFFICHAGE POINTS
    public TextMeshProUGUI countText;
    private int count;

    //ROTATION PLAYER
    public float speed = 0.02f;
    public Quaternion targetPlayerAngle;
    public int orientation = 0;
    private string rotateState="gauche";
    public CameraController cameraController;
    private Quaternion rotaCamGauche = Quaternion.Euler(0,30,0);
    private Quaternion rotaCamDroite = Quaternion.Euler(0,-30,0);
    private Quaternion rotaPlayerGauche = Quaternion.Euler(0,180,0);
    private Quaternion rotaPlayerDroite = Quaternion.Euler(0,180,0);

    //DEPLACEMENT
    public float moveSpeed;
    public bool canJump;
    public float jumph;

    //ANIMATION
    public Player player;

    //VIE
    public int life;
    public GameObject hearth1;
    public GameObject hearth2;
    public GameObject hearth3;
    public GameObject loseTextObject;
    public bool canBeHurt;

    //FIN DE GAME
    public GameObject winTextObject;
    public GameObject gemCount;
    public GameObject lifeCount;
    public TextMeshProUGUI textScore;
    public static bool levelIsFinished;

    //ATTAQUE
    public GameObject ondePrefab;
    public bool canAttack;

    //SOUND
    public AudioClip jumpSound;
    public AudioClip gemSound;
    public AudioClip winSound;
    public AudioClip lifeGainSound;
    public AudioClip hurtSound;
    public GameObject musicObject;
    void Start()
    {
        count=70;
        SetCountText();
        winTextObject.gameObject.SetActive(false);
        loseTextObject.gameObject.SetActive(false);
        targetPlayerAngle=transform.rotation;
        canJump=true;
        levelIsFinished=false;
        life=3;
        canAttack=true;
        canBeHurt=true;
    }

    void Update(){
        if(!levelIsFinished && !pauseMenu.gameIsPaused){
            //ROTATION
            if(Input.GetKeyDown(KeyCode.Q)){
                if(rotateState=="droite"){
                    ChangeCameraTargetAngle(rotaCamGauche);
                    ChangePlayerTargetAngle(rotaPlayerGauche); 
                    rotateState="gauche";
                }
            }
            if(Input.GetKeyDown(KeyCode.D)){
                if(rotateState=="gauche"){
                    ChangeCameraTargetAngle(rotaCamDroite);
                    ChangePlayerTargetAngle(rotaPlayerGauche);
                    rotateState="droite";
                }
            }
            //ONDE
            if (Input.GetKey(KeyCode.Space)) {
                if(canAttack){
                    Instantiate(ondePrefab, new Vector3(transform.position.x,transform.position.y,transform.position.z), Quaternion.identity);
                    canAttack=false;
                    StartCoroutine(resetAttack());  
                }
            }      
            transform.rotation = Quaternion.Slerp(transform.rotation, targetPlayerAngle, speed);
            manageDisplayLife();
        }
        if(life==0){
            lose();
        }
    }

    void FixedUpdate(){
        if(!levelIsFinished && !pauseMenu.gameIsPaused){
            //DEPLACEMENT
            Vector3 dp = new Vector3();
            if (Input.GetKey (KeyCode.Q)) {
                dp.x -= moveSpeed;
            }
            if (Input.GetKey (KeyCode.D)) {
                dp.x += moveSpeed;
            }
            dp = Quaternion.Euler(0, orientation, 0) * dp;
            transform.position+=dp;
            //JUMP
            if (Input.GetKey(KeyCode.Z)) {
                if(canJump){
                    GetComponent<Rigidbody>().AddForce(new Vector2(0, jumph), ForceMode.Impulse);
                    canJump=false;
                    GetComponent<AudioSource>().PlayOneShot(jumpSound, 0.15f);
                }  
            }
        }
    }

    //ROTATION PLAYER
    void ChangePlayerTargetAngle(Quaternion angle){
        targetPlayerAngle*=angle;
    }

    //ROTATION CAMERA
    void ChangeCameraTargetAngle(Quaternion angle){
        cameraController.targetCameraAngle*=angle;
    }
    
    //DETECTION TRIGGER
    private void OnTriggerEnter(Collider other){
        //GEMMES
        if(other.gameObject.CompareTag("PickUp")){
            Destroy(other.gameObject);
            count+=1;
            GetComponent<AudioSource>().PlayOneShot(gemSound);
            if(count>=100 && life<3){
                count-=100;
                life+=1;
                GetComponent<AudioSource>().PlayOneShot(lifeGainSound,0.5f);
            }
            SetCountText();

        }
        if(other.gameObject.CompareTag("PickUp5")){
            Destroy(other.gameObject);
            count+=5;
            GetComponent<AudioSource>().PlayOneShot(gemSound);
            if(count>=100 && life<3){
                count-=100;
                life+=1;
                GetComponent<AudioSource>().PlayOneShot(lifeGainSound,0.5f);
            }
            SetCountText();
        }
        if(other.gameObject.CompareTag("PickUp10")){
            Destroy(other.gameObject);
            count+=10;
            GetComponent<AudioSource>().PlayOneShot(gemSound);
            if(count>=100 && life<3){
                count-=100;
                life+=1;
                GetComponent<AudioSource>().PlayOneShot(lifeGainSound,0.5f);
            }
            SetCountText();
        }
        //WATER
        if(other.gameObject.CompareTag("Water")){
            GetComponent<Rigidbody>().AddForce(new Vector2(0, 1.2f*jumph), ForceMode.Impulse);
            canJump=false;
            player.isDamaged=true;
            life-=1;
            if(!levelIsFinished){
                GetComponent<AudioSource>().PlayOneShot(hurtSound,0.25f);
            }
        }
        //FLAG
        if(other.gameObject.CompareTag("Flag")){
            launchWinScreen();
        }
    }

    //DETECTION COLLISION
    void OnCollisionEnter(Collision infoCollision) {
        //SOL
        if (infoCollision.gameObject.CompareTag("Sol")){
            canJump=true;
            player.isDamaged=false;
            canBeHurt=true;
        }
        //ENEMY
        if(infoCollision.gameObject.CompareTag("Enemy") && canBeHurt){
            GetComponent<Rigidbody>().AddForce(new Vector2(0, 1.2f*jumph), ForceMode.Impulse);
            player.isDamaged=true;
            canJump=false;
            life-=1;
            canBeHurt=false;
            if(!levelIsFinished){
                GetComponent<AudioSource>().PlayOneShot(hurtSound, 0.25f);
            } 
        }
    }

    //AFFICHAGE DES POINTS
    void SetCountText(){
        countText.text=count.ToString();
    }

    void launchWinScreen(){
        Destroy(musicObject);
        levelIsFinished=true;
        player.isWalking=false;
        player.anim.SetBool("isWalking",false);
        gemCount.gameObject.SetActive(false);
        lifeCount.gameObject.SetActive(false);
        winTextObject.gameObject.SetActive(true);
        player.anim.SetBool("isWinning",true);
        textScore.text=""+(life*100+count*3);
        GetComponent<AudioSource>().PlayOneShot(winSound);
        
    }

    void manageDisplayLife(){
        if(life==3){
            hearth1.SetActive(true);
            hearth2.SetActive(true);
            hearth3.SetActive(true);
        }else if(life==2){
            hearth1.SetActive(true);
            hearth2.SetActive(true);
            hearth3.SetActive(false);
        }else if(life==1){
            hearth1.SetActive(true);
            hearth2.SetActive(false);
            hearth3.SetActive(false);
        }else if(life==0){
            hearth1.SetActive(false);
            hearth2.SetActive(false);
            hearth3.SetActive(false);
        }
    }

    void lose(){
        Destroy(musicObject);
        levelIsFinished=true;
        player.isWalking=false;
        player.anim.SetBool("isWalking",false);
        gemCount.gameObject.SetActive(false);
        lifeCount.gameObject.SetActive(false);
        loseTextObject.gameObject.SetActive(true);
        player.anim.SetBool("isDamaged",true);
    }

    IEnumerator resetAttack(){
        yield return new WaitForSeconds(3.0f);
        canAttack=true;
    }
}
