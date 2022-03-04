using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

  public GameObject player;
  private Vector3 cameraOffset = new Vector3(0, 3, -5);

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  // LateUpdate is called after the update method so the vehicle will move first 
  // then the camera will follow it so it wont stutter.  
  void LateUpdate()
  {
        //transform.position = player.transform.position + cameraOffset;
        transform.position = new Vector3(
            Mathf.Clamp(player.transform.position.x + cameraOffset.x, -17.11f, 65.11f),
            Mathf.Clamp(player.transform.position.y + cameraOffset.y, 0f, 61f),
            player.transform.position.z + cameraOffset.z);
    }
}
