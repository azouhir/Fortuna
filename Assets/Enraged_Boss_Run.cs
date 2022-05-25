using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enraged_Boss_Run : StateMachineBehaviour
{

    Transform player;
    Rigidbody2D rb;
    Enraged_Boss boss;

    public float speed = 5f;
    public float attackRange = 7f;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = FindObjectOfType<Player>().transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Enraged_Boss>();
    }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        try
        {
            boss.LookAtPlayer();
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPosition = Vector3.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
        }
        catch (Exception e)
        {
            rb.velocity = Vector3.zero;
            animator.SetBool("Run", false);
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
