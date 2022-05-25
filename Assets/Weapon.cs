using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform firepoint;
    public GameObject BulletPrefab;
    public AudioClip boom;

    Animator myAnimator;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
            if(FindObjectOfType<Player>().isAlive != true)
            {
                return;
            }
            Instantiate(BulletPrefab, firepoint.position, firepoint.rotation);
            FindObjectOfType<Player>().AS.Play();
            myAnimator.SetTrigger("Shoot");
    }
}
