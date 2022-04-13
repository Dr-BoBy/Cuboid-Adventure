using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bad_robot_animator : MonoBehaviour
{
    public bad_robot bad_robot;
    public Animator anim;
    void Start(){
        if(bad_robot.isRunner){
             anim.SetBool("isWalking",true);
        }
    }
}
