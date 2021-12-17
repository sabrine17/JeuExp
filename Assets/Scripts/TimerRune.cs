using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerRune : MonoBehaviour
{

    public bool desactive = true;

    Collider2D c;
    SpriteRenderer spriteRenderer;

    public ParticleSystem particules;

    // Start is called before the first frame update
    void Start()
    {
        c = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (desactive == true)
        {
            
        }

        else
        {

        }
    }

    public void Desactiver()
    {
        c.enabled = false;
        spriteRenderer.enabled = false;
        particules.Play();
        GetComponent<AudioSource>().Play();
    }

    public void Activer()
    {
        c.enabled = true;
        spriteRenderer.enabled = true;
    }

}
