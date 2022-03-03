using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    //Give enemy a max health of 100
    public int maxHealth = 100;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        //Set current health to max at the start
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        //Reduce current health by damage amount
        currentHealth -= damage;

        //Play hurt animation
        animator.SetTrigger("Hurt");

        //When health hits 0, play death animation and disable enemy
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    //Die method
    void Die()
    {
        Debug.Log("Enemy has died.");
        //Death animation
        animator.SetBool("isDead", true);

        //Disable Enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
