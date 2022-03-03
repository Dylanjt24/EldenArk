using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public float attackSpeed = 2f;
    private float attackCooldown = 0f;

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
                    enemy.GetComponent<Enemy>().currentHealth -= Damage;
                    // enemy.GetComponent<Enemy>().TakeDamage();
                }

                attackCooldown = Time.time + 1f / attackSpeed;
            }
        }
    }

    //Allows for visualization of attack radius
    void OnDrawGizmosSelected(){
        if (attackPos == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
