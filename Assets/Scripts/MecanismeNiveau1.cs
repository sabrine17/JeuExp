using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class MecanismeNiveau1 : MonoBehaviourPunCallbacks
{

    public Sprite porteOuvert;  // sprite de la porte qui est ouvert
    public Sprite porteFermer; // Sprite de la porte qui est fermer
    public Sprite levierGauche;
    public Sprite levierNormale;
    public Sprite levierDroit;

    private GameObject porte; // variable pour selectionner la porte;
    private GameObject plateformeFaible; // variable pour selectioner la plateforme à faire disparaitre
    private GameObject porte2;
    private GameObject levier;
    private GameObject plateformeLevier;
    private GameObject plateformeFantome;

    private bool mort; // variable pour vérifier la mort
    private bool victoire; // variable pour vérifier si on à gagner la partie
    private bool victoireNiveau2;

    public Text lesLumieres; // variable text pour les lumière
    private int nombreDeLumieres = 0; // variable integer pour compter le nombre de lumière
    // Start is called before the first frame update
    void Awake()
    {
        porte = GameObject.Find("door_4");
        porte2 = GameObject.Find("door_4(2)");
        plateformeFaible = GameObject.Find("faible");
        plateformeLevier = GameObject.Find("PlateformeLevier");
        levier = GameObject.Find("levier");
        plateformeFantome = GameObject.Find("PlateformeFantome");
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

        // si victoire est vrai alors....
        if(victoire == true)
        {
            // change la scène après 1 secondes
            Invoke("ChangementDeSceneVictoire", 1f);
        }
        // sinon...
        else
        {
            // victoire est fausse
            victoire = false;
        }

        if(victoireNiveau2 == true)
        {
            Invoke("ChangementDeSceneVictoireNiveau2", 1f);
        }
        else
        {
            victoireNiveau2 = false;
        }

        // si la touche 1 est appuyer alors...
        if (Input.GetKey(KeyCode.Alpha1))
        {
            // faire jouer l'animation gauche
            plateformeLevier.GetComponent<Animator>().SetBool("gauche", true);

            levier.GetComponent<SpriteRenderer>().sprite = levierGauche;
        }

        // si la touche 2 est appuyer alors...
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            // faire jouer l'animation normale
            plateformeLevier.GetComponent<Animator>().SetBool("gauche", false);
            plateformeLevier.GetComponent<Animator>().SetBool("droit", false);

            levier.GetComponent<SpriteRenderer>().sprite = levierNormale;
        }

        // si la touche 3 est appuyer alors...
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            // faire jouer l'animation droit
            plateformeLevier.GetComponent<Animator>().SetBool("droit", true);

            levier.GetComponent<SpriteRenderer>().sprite = levierDroit;
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

        // si le joueur touche l'objet qui a le nom faible alors...
        if (colision.gameObject.name == "faible")
        {
            // change de scène après 2.5 secondes
            Invoke("DetruirePlateforme", 2.5f);
        }
        // sinon...
        else if(plateformeFaible != null)
        {
            // activer la plateforme
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

            // faire jouer l'animation de victoire
            GetComponent<Animator>().SetBool("victoire", true);

            // victoire est vrai
            victoire = true;
        }
        // sinon...
        else if(porte != null)
        {
            // Si il ne touche pas la porte, alors il se ferme -- François
            porte.GetComponent<SpriteRenderer>().sprite = porteFermer;

            // ne joue pas l'animation de victoire
            GetComponent<Animator>().SetBool("victoire", false);

            // victoire est fausse
            victoire = false;
        }


        if(collision.gameObject.name == "door_4(2)")
        {
            // changer le sprite de la porte --François
            porte2.GetComponent<SpriteRenderer>().sprite = porteOuvert;

            // faire jouer l'animation de victoire
            GetComponent<Animator>().SetBool("victoire", true);

            // victoire est vrai
            victoireNiveau2 = true;
        }
        else
        {
            // Si il ne touche pas la porte, alors il se ferme -- François
            porte2.GetComponent<SpriteRenderer>().sprite = porteFermer;

            // ne joue pas l'animation de victoire
            GetComponent<Animator>().SetBool("victoire", false);

            // victoire est fausse
            victoireNiveau2 = false;
        }

        // si le joueur touche la torceh, alors...
        if (collision.gameObject.tag == "feu")
        {
            // augmenter le nombre de lumière
            nombreDeLumieres += 1 ;

            // mettre la jour la variable text
            lesLumieres.GetComponent<Text>().text = nombreDeLumieres.ToString();

            // desactiver la torche touché
            collision.gameObject.SetActive(false);
        }

        if(collision.gameObject.name == "torche(2)")
        {
            // desactiver la torche touché
            collision.gameObject.SetActive(false);
        }
    }

    // Fonction pour désactiver la plateforme faible
    void DetruirePlateforme()
    {
        // desactiver la plateforme faible
        plateformeFaible.SetActive(false);
    }

    // fonction pour recharger la scène quand on meurt
    void ChangementDeScene()
    {
        // Recharge la scene pour tout les joueurs -- François
        PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().name);
    }

    // fonction pour changer la scène lorsqu'on gagne
    void ChangementDeSceneVictoire()
    {
        // changer le order in layer du joueur
        GetComponent<Renderer>().sortingOrder = -1;

        // Changer le sprite de la porte pour qu'il puisse se fermer
        porte.GetComponent<SpriteRenderer>().sprite = porteFermer;

        // changer la scène vers le prochain niveau
        PhotonNetwork.LoadLevel("Niveau2");
    }

    void ChangementDeSceneVictoireNiveau2()
    {
        // changer le order in layer du joueur
        GetComponent<Renderer>().sortingOrder = -1;

        // Changer le sprite de la porte pour qu'il puisse se fermer
        porte2.GetComponent<SpriteRenderer>().sprite = porteFermer;

        // changer la scène vers le prochain niveau
        PhotonNetwork.LoadLevel("SceneVictoire");
    }
}
