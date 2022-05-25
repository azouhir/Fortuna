using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    float playerSpeed = 5f;
    float jumpspeed = 5f;

    public bool isAlive = true;
    public bool isFacingRight = false;
    bool ispower = false;
    float MaxHealth = 100f;
    float Health = 100f;

    public GameObject powerup;
    public GameObject levelexit;
    public Image blood;

    public GameObject runeffect;
    public GameObject jumpeffect;
    public GameObject Jumppos;
    public GameObject Runpos;

    Color bloodcolor = new Color(255f,0f,0f,1f);

    public AudioSource AS;
    public AudioSource PU;
    public AudioSource HIT;
    Animator myAnimator;
    public Rigidbody2D myRigidBody;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeet;
    float gravityScaleAtStart;

    void Awake()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex != 4)
        {
            FindObjectOfType<Healthbar>().MaxHealth(MaxHealth);
            FindObjectOfType<Healthbar>().Health(PlayerPrefs.GetFloat("Health"));
            Health = PlayerPrefs.GetFloat("Health");
        }
        else
        {
            FindObjectOfType<Healthbar>().MaxHealth(MaxHealth);
            FindObjectOfType<Healthbar>().Health(Health);
            PlayerPrefs.SetFloat("Health", Health);
            PlayerPrefs.Save();
        }

        if(currentSceneIndex == 7)
        {
            jumpspeed = 6f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        Run();
        float move = Input.GetAxis("Horizontal");
        if (move > 0 && !isFacingRight) //change !
        {
            FlipSprite();
        }
        else if(move < 0 && isFacingRight)
        {
            FlipSprite();
        }
        Jump();
        PowerUp();
        Damage();
    }

    public void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); //value is between -1 to +1  (THIS LINE IS NEEDED TO GET THE HORIZONTAL AXIS)
        Vector2 playerVelocity = new Vector2(controlThrow * playerSpeed, myRigidBody.velocity.y); 
        myRigidBody.velocity = playerVelocity;

        bool PlayerHasHorizontalVelocity = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Run", PlayerHasHorizontalVelocity);

        if (ispower == true && PlayerHasHorizontalVelocity == true)
        {
            GameObject clone = (GameObject)Instantiate(runeffect, Runpos.transform.position, Runpos.transform.rotation);
            clone.transform.SetParent(gameObject.transform);
            Destroy(clone, 1.0f);
        }
    }

    private void FlipSprite()
    {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
    }

    public void Jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Foreground","Platform")))
        {
            myRigidBody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("Jump", true);
            return;
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpspeed);
            myRigidBody.velocity += jumpVelocityToAdd;
            
            if (ispower == true)
            {
                GameObject clone = (GameObject)Instantiate(jumpeffect, Jumppos.transform.position, Jumppos.transform.rotation);
                clone.transform.SetParent(gameObject.transform);
                Destroy(clone, 1.0f);
            }
        }
        myAnimator.SetBool("Jump", false);
    }

    public void Damage()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("enemy", "Hazards", "Boss")))
        {
            Health = Health - 1f;
            FindObjectOfType<Healthbar>().Health(Health);
            PlayerPrefs.SetFloat("Health", Health);
            PlayerPrefs.Save();
            blood.color = bloodcolor;
            HIT.Play();
        }
        else
        {
            blood.color = Color.Lerp(blood.color,Color.clear, 10f * Time.deltaTime);
        }

        if (Health <= 0f)
        {
            Die();
        }
    }

    public void BulletHit()
    {
        Health = Health - 10f;
        FindObjectOfType<Healthbar>().Health(Health);
        PlayerPrefs.SetFloat("Health", Health);
        PlayerPrefs.Save();
        blood.color = bloodcolor;
        HIT.Play();

        if (Health <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
            isAlive = false;
            myAnimator.SetTrigger("Die");
            StartCoroutine(SecondsDeath());
    }

    IEnumerator SecondsDeath()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
        FindObjectOfType<Healthbar>().Health(MaxHealth);
        PlayerPrefs.SetFloat("Health", MaxHealth);
        PlayerPrefs.Save();
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void SpawnExit()
    {
        AudioSource[] allAudios = Camera.main.gameObject.GetComponents<AudioSource>();
        allAudios[0].Stop();
        allAudios[1].Play();
        levelexit.SetActive(true);
    }

    public void PowerUp()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("PowerUp")))
        {
            Health = MaxHealth;
            FindObjectOfType<Healthbar>().Health(Health);
            PU.Play();
            ispower = true;
            jumpspeed = jumpspeed * 2f;
            playerSpeed = playerSpeed * 2f;
            Destroy(powerup);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Platform"))
        {
            this.transform.parent = collision.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Platform"))
        {
            this.transform.parent = null;
        }
    }
}
