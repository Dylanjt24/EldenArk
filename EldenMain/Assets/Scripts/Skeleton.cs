using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Animator animator;
    public Transform player;
    public Transform attackPoint;
    public LayerMask playerLayer;
    public float speed;
    public float attackRange = 1.25f;
    public int attackDamage = 100;
    public float attackRate = .5f;
    public float nextAttackTime = 0f;
    public Rigidbody2D myRigidBody;


    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(animator.GetBool("Attacking") == false)
        {
            myRigidBody.velocity = new Vector2(speed, 0f);
        }
        else
        {
            myRigidBody.velocity = new Vector2(0f, 0f);
        }

        if(Time.time >= nextAttackTime && Vector2.Distance(player.position, attackPoint.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
            animator.SetBool("Attacking", true);
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        speed = -speed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(transform.localScale.x)) * 4, transform.localScale.y);
    }

    public void TakeDamage(int damage)
    {
        float playerPosition = player.localPosition.x;
        float position = myRigidBody.position.x;
        if(playerPosition < position)
        {
            if(transform.localScale.x > 0)
            {
                speed = -speed;
                FlipEnemyFacing();
            }
        }
        else
        {
            if(transform.localScale.x < 0)
            {
                speed = -speed;
                FlipEnemyFacing();
            }
        }
        currentHealth -= damage;
        animator.SetTrigger("Hit");
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if(hit != null){
            hit.GetComponent<PlayerAttack>().TakeDamage(attackDamage);
        }
        // Debug.Log("Player hit");
    }

    void StopAttack()
    {
        animator.SetBool("Attacking", false);
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    private void Die()
    {
        animator.Play("Skeleton_Dead");
        myRigidBody.velocity = new Vector2(0f, 0f);
        // animator.SetBool("IsDead", true);
        // Physics2D.SetLayerCollisionMask(playerLayer, )
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
