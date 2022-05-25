using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 2f;
    public Rigidbody2D rb;
    public GameObject impacteffect;

    CapsuleCollider2D myBodyCollider2D;

    // Start is called before the first frame update
    void Start()
    {

        if(FindObjectOfType<Player>().isFacingRight == true)
        {
            speed = 1 * speed; //change -
            print(FindObjectOfType<Player>().isFacingRight);
        }
        else
        {
            speed = -1 * speed;
            print(FindObjectOfType<Player>().isFacingRight);
        }

        rb.velocity = transform.right * speed;
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyMovement enemy = hitInfo.GetComponent<EnemyMovement>();


        if (enemy != null)
        {
            hitInfo.GetComponent<EnemyMovement>().Damage(); 
            //Collision.gameObject.GetComponent<EnemyMovement>().Die();
            Destroy(gameObject);
            GameObject clone = (GameObject)Instantiate(impacteffect, transform.position, transform.rotation);
            Destroy(clone, 1.0f);
        }

        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            Destroy(gameObject);
            GameObject clone = (GameObject) Instantiate(impacteffect, transform.position, transform.rotation);
            Destroy(clone, 1.0f);
        }

        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Boss")))
        {
            hitInfo.GetComponent<Boss>().TakeDamage(damage);
            Destroy(gameObject);
            GameObject clone = (GameObject)Instantiate(impacteffect, transform.position, transform.rotation);
            Destroy(clone, 1.0f);
        }

        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("EnragedBoss")))
        {
            hitInfo.GetComponent<Enraged_Boss>().TakeDamage(damage);
            Destroy(gameObject);
            GameObject clone = (GameObject)Instantiate(impacteffect, transform.position, transform.rotation);
            Destroy(clone, 1.0f);
        }
    }
}
