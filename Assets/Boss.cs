using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    float health = 100f;

    public Transform player;
    Animator myAnimator;
    public GameObject EnragedBoss;
    //BoxCollider2D body;
    public GameObject transformation;
    public AudioSource done;

    public bool isFlipped = false;

    void Awake()
    {
        FindObjectOfType<HealthbarBoss>().MaxHealth(100f);
        FindObjectOfType<HealthbarBoss>().Health(100f);
    }

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myAnimator.SetBool("Run", true);
        //body = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<Player>().transform;
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            isFlipped = false;
        }
        else if(transform.position.x < player.position.x && !isFlipped)
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
        myAnimator.SetTrigger("Die");
        PlayerPrefs.SetFloat("posX", this.transform.position.x);
        PlayerPrefs.SetFloat("posY", this.transform.position.y);
        done.Play();
        StartCoroutine(DeathCount());   
    }

    IEnumerator DeathCount()
    {
        yield return new WaitForSeconds(2f);
        GameObject clone = (GameObject)Instantiate(transformation, transform.position, transform.rotation);
        Destroy(clone, 1.0f);        
        Destroy(gameObject);
        EnragedBoss.SetActive(true);
    }
}
