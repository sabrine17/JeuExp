using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementPerso : MonoBehaviour
{

    public float vitesseHorizontale;
    public float vitesseVerticale;

    int jumpCount = 0;
    public int MaxJumps = 1; // maximum de saut � faire
    // Start is called before the first frame update
    void Start()
    {
        jumpCount = MaxJumps;
    }

    // Update is called once per frame
    void Update()
    {
        /////////////////////////// MOUVEMENT HORIZONTALE PERSONNAGE \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        // Mouvement du personnage vers la gauche -- Fran�ois
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // Change la vitesse de d�placement pour qu'il va vers la gauche -- Fran�ois
            vitesseHorizontale = -5f;

            // Flipper le sprite sur l'axe des X -- Fran�ois
            GetComponent<SpriteRenderer>().flipX = true;
        }

        // Mouvement du personnage vers la droite -- Fran�ois
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // Change la vitesse de d�placement pour qu'il va vers la droite -- Fran�ois
            vitesseHorizontale = 5f;

            // Flipper le sprite sur l'axe des X -- Fran�ois
            GetComponent<SpriteRenderer>().flipX = false;
        }

        // la vitesse de d�placement en fonction de la v�locit� du personnage -- Fran�ois
        else
        {
            vitesseHorizontale = GetComponent<Rigidbody2D>().velocity.x;
        }

        /////////////////////////// MOUVEMENT VERTICALE PERSONNAGE \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        // Mouvement du personnage vers le haut (SAUT) en appuyant sur W ou la fl�che pointant vers le haut-- Fran�ois
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (jumpCount > 0)
            {
                // Change la vitesse de d�placement pour qu'il Saute -- Fran�ois
                vitesseVerticale = 20f;

                // Activer l'animation de saut du personnage -- Fran�ois
                GetComponent<Animator>().SetBool("saut", true);

                jumpCount -= 1;
            }

        }

        // Mouvement du personnage vers le haut (SAUT) en retirant la pression sur W ou la fl�che pointant vers le haut-- Fran�ois
        /*else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            // D�sactiver l'animation de saut -- Fran�ois
            GetComponent<Animator>().SetBool("saut", false);
        }*/

        // la vitesse du saut en fonction de la v�locit� du personnage -- Fran�ois
        else
        {
            vitesseVerticale = GetComponent<Rigidbody2D>().velocity.y;
        }

        // on va changer la v�locit� du personnage. -- Fran�ois
        GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseHorizontale, vitesseVerticale);

        // Activer l'animation de marche si la vitesse horizontale est plus grand que 0.1f
        if (vitesseHorizontale > 0.1f || vitesseHorizontale < -0.1f)
        {
            GetComponent<Animator>().SetBool("marche", true);
        }
        // Desactiver l'animation de marche si la vitesse horizontale est plus petit que 0.1f
        else
        {
            GetComponent<Animator>().SetBool("marche", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Sol")
        {
            jumpCount = MaxJumps;
            GetComponent<Animator>().SetBool("saut", false);
        }
    }
}
