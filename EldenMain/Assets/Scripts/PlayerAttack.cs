using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    //Defining animation
    public Animator animator;

    //Create Attack Position variable
    public Transform attackPos;

    //Create variable for layer where enemies are
    public LayerMask EnemyLayer;

    //Create variable for attack range and damage
    public float attackRange;
    public int Damage;

    public int maxHealth = 100;
    private int currentHealth;

    public float attackSpeed = 2f;
    private float attackCooldown = 0f;

    private void Start() {
        currentHealth = maxHealth;
    }
    public void Attack(InputAction.CallbackContext context)
    {
        //If attack is off cooldown and the attack button is pressed:
        if(context.performed)
        {
            if(Time.time >= attackCooldown)
            {
                //Play an attack animation
                animator.SetTrigger("Attack");

                //Detect enemies in range of the attack
                Collider2D[] damageArea = Physics2D.OverlapCircleAll(attackPos.position, attackRange, EnemyLayer);

                //Damage the enemies
                foreach(Collider2D enemy in damageArea)
                {
                    // enemy.GetComponent<Enemy>().CurrentHealth -= Damage;
                    enemy.GetComponent<Skeleton>().TakeDamage(Damage);
                    Debug.Log("Skeleton hit");
                }

                attackCooldown = Time.time + 1f / attackSpeed;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(currentHealth);
        currentHealth -= damage;
        Debug.Log(currentHealth);
        animator.SetTrigger("Hit");
        if(currentHealth <= 0)
        {
            Debug.Log("Player hit");
            Die();
        }
    }

    private void Die()
    {
        GetComponent<PlayerMovement>().enabled = false;
        Debug.Log("should be dead");
        animator.SetBool("IsDead", true);
        // animator.Play("Player_Dead");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("MainMenu");
    }


    //Allows for visualization of attack radius
    void OnDrawGizmosSelected(){
        if (attackPos == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
