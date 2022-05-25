using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Normal_Run : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    Boss boss;

    public float speed = 2.5f;
    public float attackRange = 1.5f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = FindObjectOfType<Player>().transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        try
        {
            boss.LookAtPlayer();
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPosition = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);
            rb.MovePosition(newPosition);

            if (Vector2.Distance(player.position, rb.position) <= attackRange)
            {
                animator.SetTrigger("Attack");
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            rb.velocity = Vector3.zero;
            animator.SetBool("Run", false);
        }
    }
}
