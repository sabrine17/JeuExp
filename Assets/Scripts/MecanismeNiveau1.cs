using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class MecanismeNiveau1 : MonoBehaviourPunCallbacks
{
    public Sprite porteOuvert; // sprite de la porte qui est ouvert
    public Sprite porteFermer; // Sprite de la porte qui est fermer

    public GameObject porte; // variable pour selectionner la porte;
    public GameObject plateformeFaible;

    private bool mort; // variable pour vérifier la mort
    private bool victoire;

    public Text lesLumieres;
    private int nombreDeLumieres = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (lesLumieres != null)
        {
            lesLumieres.text = "Torches : " + nombreDeLumieres + "/2";
        }


        // vérifier si le personnage meurt -- François
        if (mort == true)
        {
            // Activation de l'animation de mort --François
            GetComponent<Animator>().SetBool("mort", true);

            // Désactiver le collider -- François
            GetComponent<BoxCollider2D>().enabled = false;

            // Empecher que le personnage bouge quand il est mort -- François
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;

            // desactiver le scale qui se truve dans le script mouvement perso pour pas qu'il se tourne -- François
            GetComponent<MouvementPerso>().transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
        }

        if(victoire == true)
        {
            Invoke("ChangementDeSceneVictoire", 1f);
        }
        else
        {
            victoire = false;
        }

    }

    void OnCollisionEnter2D(Collision2D colision)
    {
        // vérifier si le personnage touche l'objet avec le tag danger (ce sont les pics) -- François
        if (colision.gameObject.tag == "Danger")
        {
            // mettre la variable booléene mort à true -- François
            mort = true;

            // Change la scène après 3 secondes
            Invoke("ChangementDeScene", 3f);
        }
        else
        {
            // si le personnage n'a pas toucher au pics alors il n'est pas mort --François
            mort = false;
        }

        if (colision.gameObject.name == "faible")
        {
            Invoke("DetruirePlateforme", 2.5f);
        }
        else
        {
            plateformeFaible.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // vérifier si le personnage entre en collision avec la porte
        if(collision.gameObject.name == "door_4")
        {
            // changer le sprite de la porte --François
            porte.GetComponent<SpriteRenderer>().sprite = porteOuvert;
            GetComponent<Animator>().SetBool("victoire", true);
            victoire = true;
        }
        else
        {
            // Si il ne touche pas la porte, alors il se ferme -- François
            porte.GetComponent<SpriteRenderer>().sprite = porteFermer;
            GetComponent<Animator>().SetBool("victoire", false);
            victoire = false;
        }

        if(collision.gameObject.tag == "feu")
        {
            nombreDeLumieres += 1 ;
            lesLumieres.GetComponent<Text>().text = nombreDeLumieres.ToString();
            collision.gameObject.SetActive(false);
        }
    }

    void ChangementDeScene()
    {
        // Recharge la scene pour tout les joueurs -- François
        PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().name);
    }

    void ChangementDeSceneVictoire()
    {
        GetComponent<Renderer>().sortingOrder = -1;
        porte.GetComponent<SpriteRenderer>().sprite = porteFermer;
        PhotonNetwork.LoadLevel("Niveau2");
    }

    void DetruirePlateforme()
    {
        plateformeFaible.SetActive(false);
    }
}
