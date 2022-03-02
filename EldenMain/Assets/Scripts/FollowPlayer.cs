using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

  public GameObject player;
  private Vector3 cameraOffset = new Vector3(0, 0, -5);

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  // LateUpdate is called after the update method so the vehicle will move first 
  // then the camera will follow it so it wont stutter.  
  void LateUpdate()
  {
    transform.position = player.transform.position + cameraOffset;
  }
}
