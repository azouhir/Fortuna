using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enraged_Boss : MonoBehaviour
{
    float health = 100f;

    public Transform player;
    Animator myAnimator;
    public Rigidbody2D myRigidBody;
    public bool isAlive = true;
    public AudioSource AS;

    public Vector3 prevpos;
    public bool isFlipped = false;

    void Awake()
    {
        FindObjectOfType<HealthbarBoss>().MaxHealth(100f);
        FindObjectOfType<HealthbarBoss>().Health(100f);
        transform.position = new Vector3(PlayerPrefs.GetFloat("posX"), PlayerPrefs.GetFloat("posY"), 0f);
    }

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myAnimator.SetBool("Run", true);
        player = FindObjectOfType<Player>().transform;
        myRigidBody = GetComponent<Rigidbody2D>();
        AS = GetComponent<AudioSource>();
    }

    void Update()
    {
        float distance = Vector2.Distance(player.position, myRigidBody.position);
        Debug.Log(distance);

        if (distance > 7f)
        {
            myAnimator.SetBool("Run", true);
        }
        else
        {
            myAnimator.SetBool("Run", false);
        }
    }

    public float Distance()
    {
        float distance = Vector2.Distance(player.position, myRigidBody.position);
        return distance;
    }

        public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void TakeDamage(float damage)
    {
        if (health > 0f)
        {
            health = health - damage;
            FindObjectOfType<HealthbarBoss>().Health(health);
        }

        else
        {
            Die();
        }
    }

    void Die()
    {
        myAnimator.SetBool("Run", false);
        isAlive = false;
        myAnimator.SetTrigger("Die");

        StartCoroutine(DeathCount());
    }

    IEnumerator DeathCount()
    {
        yield return new WaitForSeconds(0.8f);
        FindObjectOfType<Player>().SpawnExit();
        Destroy(gameObject);
    }
}
