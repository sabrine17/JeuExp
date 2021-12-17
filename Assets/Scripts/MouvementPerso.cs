using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


public class MouvementPerso : MonoBehaviourPunCallbacks
{

    public float vitesseHorizontale;
    public float vitesseVerticale;

    public float sautNormal;
    public float sautRune;

    public int jumpCount = 0;
    public int MaxJumps = 1; // maximum de saut à faire
    // Start is called before the first frame update
    void Start()
    {

        PhotonNetwork.OfflineMode = false;

        if (gameObject.name == "PersoPrincipal_1")
        {
            GameObject spawn = GameObject.Find("SpawnJ1");
            gameObject.transform.position = spawn.transform.position;


        }

        else if (gameObject.name == "PersoSecondaire_1")
        {
            GameObject spawn = GameObject.Find("SpawnJ2");
            gameObject.transform.position = spawn.transform.position;
        }

        jumpCount = MaxJumps;
    }

    // Update is called once per frame
    void Update()
    {

        if (photonView.IsMine)
        {
            /////////////////////////// MOUVEMENT HORIZONTALE PERSONNAGE \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            // Mouvement du personnage vers la gauche -- François
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                // Change la vitesse de déplacement pour qu'il va vers la gauche -- François
                vitesseHorizontale = -5f;

                // Flipper le sprite sur l'axe des X -- François
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            // Mouvement du personnage vers la droite -- François
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                // Change la vitesse de déplacement pour qu'il va vers la droite -- François
                vitesseHorizontale = 5f;

                // Flipper le sprite sur l'axe des X -- François
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
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
                if (jumpCount > 0)
                {

                    GetComponent<sonPerso>().sautJoueAudio();

                    // Change la vitesse de déplacement pour qu'il Saute -- François
                    if (GetComponent<InteractionRunes>().runeSaut == false)
                    {
                         vitesseVerticale = sautNormal;
                    }

                    else
                    {
                        vitesseVerticale = sautRune;
                    }

                    // Activer l'animation de saut du personnage -- François
                    GetComponent<Animator>().SetBool("saut", true);

                    jumpCount -= 1;

                }

            }

            // Mouvement du personnage vers le haut (SAUT) en retirant la pression sur W ou la flèche pointant vers le haut-- François
            /*else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                // Désactiver l'animation de saut -- François
                GetComponent<Animator>().SetBool("saut", false);
            }*/

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Sol" || collision.gameObject.tag == "Joueur")
        {
            jumpCount = MaxJumps;
            GetComponent<Animator>().SetBool("saut", false);
        }
    }
}
