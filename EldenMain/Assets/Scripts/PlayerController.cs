using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

  public float speed = 25.0f;
  public float jumpHeight = 20.0f;

  private float horizontalMovement;

  private float verticalMovement;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    horizontalMovement = Input.GetAxis("Horizontal");
    verticalMovement = Input.GetAxis("Vertical");

    transform.Translate(Vector2.right * Time.deltaTime * speed * horizontalMovement);

    if (Input.GetKeyDown(KeyCode.Space))
    {
      GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
    }

    if (Input.GetKeyDown(KeyCode.LeftShift))
    {
      speed += 5.0f;
    }
    else if (Input.GetKeyUp(KeyCode.LeftShift))
    {
      speed -= 5.0f;
    }
  }
}
