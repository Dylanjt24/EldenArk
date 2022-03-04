using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    private CheckPointMaster cm;

    void Start()
    {
        cm = GameObject.FindGameObjectWithTag("CheckMaster").GetComponent<CheckPointMaster>();
        transform.position = cm.lastCheckPointPos;
    }

}
