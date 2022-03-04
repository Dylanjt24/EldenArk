using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
  PlayerAttack playerHealth;

  //   public Animator animator;

  public int healAmount = 25;

  private void Awake()
  {
    playerHealth = FindObjectOfType<PlayerAttack>();

  }

  private void OnTriggerEnter2D(Collider2D col)
  {
    if (playerHealth.currentHealth < playerHealth.maxHealth)
    {
      Destroy(gameObject);
      playerHealth.currentHealth = playerHealth.currentHealth + healAmount;
    }
  }
}
