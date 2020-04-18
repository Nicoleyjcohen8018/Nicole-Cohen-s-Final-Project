using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerscript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public float jump;

    public Text score;

    public Text Win;

    public Text Lives;

    public int scoreValue = 0;

    private int liveValue;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;

    Animator anim;

    private bool facingRight = true;

    private bool isOnGround;

    private bool inAir;

    public Transform groundcheck;

    public float checkRadius;

    public LayerMask allGround;

    private bool moved;

    public Timerscript Timer;

    // Start is called before the first frame update
    void Start()
    {
        liveValue = 3;

        anim = GetComponent<Animator>();
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        Lives.text = "Lives: " + liveValue.ToString();

        musicSource.clip = musicClipOne;
        musicSource.Play();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

    }
    
    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 && inAir == false) 
        {
            anim.SetInteger("state", 2);
        }
        if(Input.GetAxis("Horizontal") == 0 && inAir == false)
        {
            anim.SetInteger("state", 0);
        }
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    //Function that checks if we hit something
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Score();
        }
        if (collision.collider.tag == "enemy")
        {
            liveValue -- ;
            Lives.text = "Lives: " + liveValue.ToString();
            Score();

        }

        if (collision.collider.tag == "potion")
        {
            jump = jump * 1.5f;
            Destroy(collision.collider.gameObject);
        }

    }

private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                inAir = true;
                anim.SetInteger("state", 1);
                rd2d.AddForce(new Vector2(0, 2 * jump), ForceMode2D.Impulse);
            }
            else
            {
                inAir = false;
                anim.SetInteger("state", 0);
            }
        }
    }

    public void Score()
    {
        if (scoreValue == 4 && moved == false)
        {
            liveValue = 3;
            Lives.text = "Lives: " + liveValue.ToString();
            transform.position = new Vector2(49f, 10f);
            moved = true;
        }

        if (scoreValue >= 8 && liveValue >= 1)
        {
            Win.text = "You win!!! Game created by Nicole Cohen";
            musicSource.clip = musicClipOne;
            musicSource.Stop();
            musicSource.clip = musicClipTwo;
            musicSource.Play();
           
        }
        if (liveValue == 0)

         {
            Win.text = "You lose!!! Game created by Nicole Cohen";
            rd2d.bodyType = RigidbodyType2D.Static;
            gameObject.GetComponent<Renderer>().enabled = false;
         }
    }

    public void GameOver()
    { 
            Win.text = "You lose!!! Game created by Nicole Cohen";
            rd2d.bodyType = RigidbodyType2D.Static;
            gameObject.GetComponent<Renderer>().enabled = false;
    }


}
