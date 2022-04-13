using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    void Start(){
         transform.Rotate(new Vector3(0,0,Random.Range(0,90)));
    }
    void Update()
    {
        transform.Rotate(new Vector3(0,0,60)*Time.deltaTime);
    }
}
