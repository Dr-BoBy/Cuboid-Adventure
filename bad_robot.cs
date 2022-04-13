using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bad_robot : MonoBehaviour{

    //JUMP
    public bool isJumper;
    public float jumpDelay;
    public float jumph;
    
    //RUN
    public bool isRunner;
    public float speed;
    private Quaternion targetRotation;

    void Start(){
        if(isJumper){
            StartCoroutine(jumpCycle());
        }
        targetRotation=Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }

    void Update(){
        
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,100*Time.deltaTime);
    }

    void FixedUpdate(){
        if(isRunner){
            Vector3 dp = new Vector3();
            dp.x -= speed;
            dp = Quaternion.Euler(0, transform.eulerAngles.y, 0) * dp;
            transform.position+=dp;
        }  
    }
    //DETECTION TRIGGER
    private void OnTriggerEnter(Collider other){
        //ONDE
        if(other.gameObject.CompareTag("Onde")){
            Destroy(gameObject);
        }
    }
    //DETECTION COLLISION
    void OnCollisionEnter(Collision infoCollision) {
        if (infoCollision.gameObject.CompareTag("Wall")){
            Vector3 dp = new Vector3();
            dp.x += 0.15f;
            dp = Quaternion.Euler(0, transform.eulerAngles.y, 0) * dp;
            transform.position+=dp;
            targetRotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
        }
    }

    IEnumerator jumpCycle(){
        while(true){
            yield return new WaitForSeconds(jumpDelay);
            GetComponent<Rigidbody>().AddForce(new Vector2(0, jumph), ForceMode.Impulse);
        }   
    }

}
