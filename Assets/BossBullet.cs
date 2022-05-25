using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBullet : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 2f;
    public Rigidbody2D rb;
    public GameObject impacteffect;

    Color bloodcolor = new Color(255f, 0f, 0f, 1f);

    CapsuleCollider2D myBodyCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            hitInfo.GetComponent<Player>().BulletHit();
            Destroy(gameObject);
            GameObject clone = (GameObject)Instantiate(impacteffect, transform.position, transform.rotation);
            Destroy(clone, 1.0f);
            FindObjectOfType<Player>().blood.color = bloodcolor;
        }
        else
        {
            FindObjectOfType<Player>().blood.color = Color.Lerp(FindObjectOfType<Player>().blood.color, Color.clear, 10f * Time.deltaTime);
        }

        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            Destroy(gameObject);
            GameObject clone = (GameObject)Instantiate(impacteffect, transform.position, transform.rotation);
            Destroy(clone, 1.0f);
        }
    }
}
