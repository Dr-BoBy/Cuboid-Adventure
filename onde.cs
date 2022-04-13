using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onde : MonoBehaviour
{
    private float size;
    void Start()
    {
        size=0.1f;
        StartCoroutine(sizeCycle());
    }

    void Update(){
        Vector3 newScale = new Vector3(size,size,size); 
        transform.localScale = Vector3.Lerp(transform.localScale, newScale, Time.deltaTime*3);
    }

    IEnumerator sizeCycle(){
        while(true){
            yield return new WaitForSeconds(0.05f);
            size+=0.1f;
            if(size>2f){
                Destroy(gameObject);
            }
        }
    }
}
