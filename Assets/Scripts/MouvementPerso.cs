using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementPerso : MonoBehaviour
{

    public float vitesseHorizontale;
    public float vitesseVerticale;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /////////////////////////// MOUVEMENT HORIZONTALE PERSONNAGE \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        // Mouvement du personnage vers la gauche -- François
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            // Change la vitesse de déplacement pour qu'il va vers la gauche -- François
            vitesseHorizontale = -5f;

            // Flipper le sprite sur l'axe des X -- François
            GetComponent<SpriteRenderer>().flipX = true;
        }

        // Mouvement du personnage vers la droite -- François
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            // Change la vitesse de déplacement pour qu'il va vers la droite -- François
            vitesseHorizontale = 5f;

            // Flipper le sprite sur l'axe des X -- François
            GetComponent<SpriteRenderer>().flipX = false;
        }

        // la vitesse de déplacement en fonction de la vélocité du personnage -- François
        else
        {
            vitesseHorizontale = GetComponent<Rigidbody2D>().velocity.x;
        }

        /////////////////////////// MOUVEMENT VERTICALE PERSONNAGE \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        // Mouvement du personnage vers le haut (SAUT) en appuyant sur W ou la flèche pointant vers le haut-- François
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Change la vitesse de déplacement pour qu'il Saute -- François
            vitesseVerticale = 20f;

            // Activer l'animation de saut du personnage -- François
            GetComponent<Animator>().SetBool("saut", true);
        }

        // Mouvement du personnage vers le haut (SAUT) en retirant la pression sur W ou la flèche pointant vers le haut-- François
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            // Désactiver l'animation de saut -- François
            GetComponent<Animator>().SetBool("saut", false);
        }

        // la vitesse du saut en fonction de la vélocité du personnage -- François
        else
        {
            vitesseVerticale = GetComponent<Rigidbody2D>().velocity.y;
        }

        // on va changer la vélocité du personnage. -- François
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
}
