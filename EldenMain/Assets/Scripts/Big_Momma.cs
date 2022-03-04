using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big_Momma : MonoBehaviour
{
    public int maxHealth = 500;
    private int currentHealth;
    public Animator animator;
    public Transform player;
    public Transform FrontAttackPoint;
    public Transform AreaAttackPoint;
    public Transform CastPoint;
    public float SightRange;
    public LayerMask playerLayer;
    public float speed = 3f;
    public float FrontAttackRange = 2.14f;
    public float AreaAttackRange = 4.55f;
    public int FrontAttackDamage = 100;
    public int AreaAttackDamage = 50;
    public float attackRate = .5f;
    public float nextAttackTime = 0f;
    public Rigidbody2D myRigidBody;
    private bool isAgro = false;

    private bool isSearching = false;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // if(player.position.x < CastPoint.position.x)
        // {
        //     if(transform.localScale.x < 0)
        //     {
        //         FlipEnemyFacing();
        //     }
        // }
        // if(player.position.x > CastPoint.position.x)
        // {
        //     if(transform.localScale.x > 0)
        //     {
        //         FlipEnemyFacing();
        //     }
        // }
        if(CanSeePlayer(SightRange))
        {
            isAgro = true;
        }
        else
        {
            if(isAgro)
            {
                if(!isSearching)
                {
                    isSearching = true;
                    Invoke("StopChasingPlayer", 10);
                }
            }
        }

        if(isAgro)
        {
            if(animator.GetBool("Attacking") == true)
            {
                StopChasePlayer();
            }
            else
            {
                animator.SetBool("IsAgro", true);
                ChasePlayer();
            }
        }


        if(Time.time >= nextAttackTime && Vector2.Distance(player.position, FrontAttackPoint.position) <= FrontAttackRange)
        {
            animator.SetTrigger("Front_Attack");
            animator.SetBool("Attacking", true);
            nextAttackTime = Time.time + 1f / attackRate;
        }

        if(Time.time >= nextAttackTime && Vector2.Distance(player.position, AreaAttackPoint.position) <= AreaAttackRange)
        {
            animator.SetTrigger("Area_Attack");
            animator.SetBool("Attacking", true);
            nextAttackTime = Time.time + 1f / attackRate;
        }

    bool CanSeePlayer(float distance)
    {
        bool val = false;
        float range = distance;
        if(transform.localScale.x > 0)
        {
            range = -distance;
        }
        Vector2 endPos = CastPoint.position + Vector3.right * range;
        RaycastHit2D hit = Physics2D.Linecast(CastPoint.position, endPos, 1 << LayerMask.NameToLayer("Player"));
        if(hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }
        }
        return val;
    }

    }
    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(transform.localScale.x)) * 15, transform.localScale.y);
    }

    void ChasePlayer()
    {
        if(myRigidBody.position.x < player.position.x)
        {
            if(transform.localScale.x > 0)
            {
                FlipEnemyFacing();
            }
            myRigidBody.velocity = new Vector2(speed, 0f);
        }
        else
        {
            if(transform.localScale.x < 0)
            {
                FlipEnemyFacing();
            }
            myRigidBody.velocity = new Vector2(-speed, 0f);
        }
    }

    void StopChasePlayer()
    {
        myRigidBody.velocity = new Vector2(0f, 0f);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hit");
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void FrontAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(FrontAttackPoint.position, FrontAttackRange, playerLayer);
        if(hit != null){
            hit.GetComponent<PlayerAttack>().TakeDamage(FrontAttackDamage);
        }
        // Debug.Log("Player hit");
    }

    void AreaAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(AreaAttackPoint.position, AreaAttackRange, playerLayer);
        if(hit != null){
            hit.GetComponent<PlayerAttack>().TakeDamage(AreaAttackDamage);
        }
        // Debug.Log("Player hit");
    }

    void StopAttack()
    {
        animator.SetBool("Attacking", false);
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.DrawWireSphere(FrontAttackPoint.position, FrontAttackRange);
        Gizmos.DrawWireSphere(AreaAttackPoint.position, AreaAttackRange);
        Gizmos.DrawLine(CastPoint.position, CastPoint.position + Vector3.right * SightRange);
    }

    private void Die()
    {
        animator.SetTrigger("Dead");
        myRigidBody.velocity = new Vector2(0f, 0f);
        // animator.SetBool("IsDead", true);
        // Physics2D.SetLayerCollisionMask(playerLayer, )
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

}
