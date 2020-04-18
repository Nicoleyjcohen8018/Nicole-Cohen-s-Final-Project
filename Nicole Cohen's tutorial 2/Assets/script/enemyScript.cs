using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    // Transforms to act as start and end markers for the journey.
    public Transform startMarker;
    public Transform endMarker;
    public AudioSource MusicSource;
    public AudioClip Scream;
    public Collider2D Dino;
    public SpriteRenderer Dino1;
    Animator anim;

    // Movement speed in units/sec.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    Vector3 previousPosition;

    void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);

        anim = GetComponent <Animator>();

    }

    // Follows the target position like with a spring
    void Update()
    {
        // Distance moved = time * speed.
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers and pingpong the movement.
        transform.position = Vector3.Lerp(startMarker.position, endMarker.position, Mathf.PingPong(fracJourney, 1));

        Vector3 currentPosition = transform.position;
        bool facingRight = currentPosition.x - previousPosition.x > 0;
        previousPosition = currentPosition;

        if (facingRight == false)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            anim.SetInteger("condition", 1);
       }
        else if (facingRight == true)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            anim.SetInteger("condition", 1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Player")
        {
            MusicSource.clip = Scream;
            MusicSource.Play();

            Dino1.enabled = false;
            Dino.enabled = false;

        }
    }

}
