using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private CheckPointMaster cm;

    void Start(){
        cm = GameObject.FindGameObjectWithTag("CheckMaster").GetComponent<CheckPointMaster>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            cm.lastCheckPointPos = transform.position;
        }
    }
}
