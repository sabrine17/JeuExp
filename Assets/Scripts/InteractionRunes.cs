using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionRunes : MonoBehaviour
{
    GameObject derniereRuneTouche;

    public bool runeSaut = false;
    public bool runeRapetisse = true;

    int timer = 0;
    public Text txtTimer;
    public GameObject positionTimer;

    Coroutine derniereCoroutine;

    //Variables pour la rune de rapetissage
    float scaleDesire;
    public float scaleNormal;
    public float scaleRapetisse;

    Transform transformJoueur;

    // Start is called before the first frame update
    void Start()
    {
        transformJoueur = gameObject.transform;

        runeSaut = false;
        runeRapetisse = false;
        txtTimer.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (timer == 0)
        {
            
        }

        else
        {
            txtTimer.text = timer.ToString();
        }

        Vector3 posTxtTimer = Camera.main.WorldToScreenPoint(positionTimer.transform.position);
        txtTimer.gameObject.transform.position = posTxtTimer;

        //Fix si jamais le timer se retrouve en dessous de 0
        if (timer < 0)
        {
            timer = 0;
        }

        /*if (runeRapetisse == true && transformJoueur.localScale.x > scaleDesire)
        {
            gameObject.transform.localScale = new Vector2 (transformJoueur.localScale.x - 0.01f, transformJoueur.localScale.y - 0.1f);
        }

        else if (runeRapetisse == false && transformJoueur.localScale.x < scaleDesire)
        {
            print("hello");
            gameObject.transform.localScale = new Vector2(transformJoueur.localScale.x + 0.01f, transformJoueur.localScale.y + 0.1f);
        }*/

    }

    void ResetTimer(int temps) //Cette fonction sert à s'assurer de mettre toutes les propriétés par défaut
                               //que ca soit a la fin du timer ou avant d'appliquer un nouvel effet de rune
    {
        runeSaut = false;
        runeRapetisse = false;
        scaleDesire = scaleNormal;
        //transformJoueur.localScale = new Vector2(scaleDesire, scaleDesire);

        txtTimer.text = ""; //On efface le texte du timer pour ne pas qu'il s'affiche
        timer = 0;
        timer = temps; //La variable de la longueur du timer est définie lorsqu'on appelle la fonction dans le TriggerEnter

        if (temps != 0)
        {
            derniereCoroutine = StartCoroutine(TimerSecoule()); //On part le decoulement du timer
        }

        loopReactiveRunes();

        if (derniereRuneTouche != null)
        {
            derniereRuneTouche.GetComponent<TimerRune>().Desactiver(); //Si le joueur a touche a une rune recemment, on la desactive pour quelle disparaisse du world
        }
    }

    void RuneSaut()
    {
        runeSaut = true; //Cette variable est lu dans le script du mouvement joueur pour determiner la hauteur du saut
    }

    void RuneRapetisse()
    {
        runeRapetisse = true;
        scaleDesire = scaleRapetisse;
        transformJoueur.localScale = new Vector2 (scaleDesire, scaleDesire);
    }

    IEnumerator TimerSecoule() //Cette fonction gère le chronometrage
    {
        if (timer > 0) //Si il reste des secondes au timer, il sen preleve une
        {
            yield return new WaitForSeconds(1); //Attend une seconde
            timer -= 1;
            derniereCoroutine = StartCoroutine(TimerSecoule()); //Sapelle lui même, agis comme une loop
        }

        if (timer == 0) //Si le timer est fini ou reseté
        {

            if (derniereRuneTouche != null) //Lorque le timer atteint 0 alors que le joueur est encore sous leffet d'une rune
                                            //la loop qui reactive toutes les runes et appelée
            {
                loopReactiveRunes();

                if (derniereCoroutine != null)
                {
                    StopCoroutine(derniereCoroutine);
                }

                derniereRuneTouche = null;
                ResetTimer(0);
            }

        }

    }

    void loopReactiveRunes() //Cette loop sert a activer toutes les runes dans la scene.
                             //Comme ca, lorsque le joueur intergis avec une nouvelle rune, les autres reapparaissent immediatement
    {
        GameObject[] mesRunes = GameObject.FindGameObjectsWithTag("Rune");
        int i = 0;

        while (i < mesRunes.Length)
        {
            mesRunes[i].GetComponent<TimerRune>().Activer();
            i++;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Rune") //Quand le joueur entre en contact avec une rune, celle ci est storé dans une variable
        {
            derniereRuneTouche = collision.gameObject;
            //derniereRuneTouche.GetComponent<TimerRune>().desactive = true;
            if (derniereCoroutine != null)
            {
                StopCoroutine(derniereCoroutine);
            }
        }

        if (collision.name == "RuneSaut") //Appelle la fonction spécifique à la rune saut
        {
            ResetTimer(3); //Cette rune a un timer de 3s
            RuneSaut();
        }

        else if (collision.name == "RuneRapetisse") //Appelle la fonction spécifique à la rune de rapetissage 
        {
            ResetTimer(7); //Cette rune a un timer de 7s
            RuneRapetisse();
        }
    }
}
