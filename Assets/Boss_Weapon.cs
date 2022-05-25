using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Weapon : MonoBehaviour
{
    public Transform firepoint;
    public GameObject BulletPrefab;

    public float nextShot = 0f;

    Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Enraged_Boss>().Distance() <= 7f)
        {
            if (Time.time > nextShot)
            {
                Shoot();
                nextShot = Time.time + 0.5f;
            }
        }
    }

    void Shoot()
    {
        if (!FindObjectOfType<Enraged_Boss>().isAlive)
        {
            return;
        }
        myAnimator.SetTrigger("Attack");
        Instantiate(BulletPrefab, firepoint.position, firepoint.rotation);
        FindObjectOfType<Enraged_Boss>().AS.Play();
    }
}
