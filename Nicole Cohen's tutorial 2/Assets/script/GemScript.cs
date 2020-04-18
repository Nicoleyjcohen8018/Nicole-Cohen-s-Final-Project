using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    public AudioSource MusicSource;
    public AudioClip magic;
    public Collider2D Gem;
    public SpriteRenderer Gem1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Player")
        {
            MusicSource.clip = magic;
            MusicSource.Play();

            Gem.enabled = false;
            Gem1.enabled = false;

        }
    }
}
