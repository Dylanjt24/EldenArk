using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{

  public AudioSource uiSound;

  public GameObject arrow;

  public AudioClip sound;
  // Start is called before the first frame update
  public void PlaySound()
  {
    uiSound.PlayOneShot(sound);
  }

}
