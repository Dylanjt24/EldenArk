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
        transform.position = player.transform.position + cameraOffset;

        if (transform.position.x < -12.9f)
            transform.position = new Vector3(-12.9f, transform.position.y, transform.position.z);

        if (transform.position.x > 60.9f)
            transform.position = new Vector3(60.9f, transform.position.y, transform.position.z);

        if (transform.position.y > 61f)
            transform.position = new Vector3(transform.position.x, 61f, transform.position.z);

        if (transform.position.y < 0f)
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
  }
}
