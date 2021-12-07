using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerRune : MonoBehaviour
{

    public bool desactive = true;

    Collider2D collider;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (desactive = true)
        {
            
        }

        else
        {

        }
    }

    public void Desactiver()
    {
        collider.enabled = false;
        spriteRenderer.enabled = false;
    }

    public void Activer()
    {
        collider.enabled = true;
        spriteRenderer.enabled = true;
    }

}
