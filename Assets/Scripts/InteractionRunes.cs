using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionRunes : MonoBehaviour
{
    GameObject derniereRuneTouche;

    public bool runeSaut = false;
    public bool runeRapetisse = true;

    bool peutResetRunes;

    int timer = 0;
    public Text txtTimer;
    public GameObject positionTimer;

    Coroutine derniereCoroutine;

    // Start is called before the first frame update
    void Start()
    {
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

        //Fix de bug bizarre ne devrait pas etre final
        if (timer < 0)
        {
            timer = 0;
        }

    }

    void ResetTimer(int temps) //Cette fonction sert à s'assurer de mettre toutes les propriétés par défaut
                               //que ca soit a la fin du timer ou avant d'appliquer un nouvel effet de rune
    {
        runeSaut = false;
        runeRapetisse = false;
        txtTimer.text = ""; //On efface le texte du timer pour ne pas qu'il s'affiche
        timer = 0;
        timer = temps; //La variable de la longueur du timer est définie lorsqu'on appelle la fonction dans le TriggerEnter

        if (temps != 0)
        {
            derniereCoroutine = StartCoroutine(TimerSecoule()); //On part le decoulement du timer
        }

        //Cette loop sert a activer toutes les runes dans la scene. Comme ca, lorsque le joueur intergis avec une nouvelle rune, les autres reapparaissent immediatement
        GameObject[] mesRunes = GameObject.FindGameObjectsWithTag("Rune");
        int i = 0;
        while (i < mesRunes.Length)
        {
            mesRunes[i].GetComponent<TimerRune>().Activer();
            i++;
        }

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
    }

    IEnumerator TimerSecoule()
    {
        if (timer > 0) //Si il reste des secondes au timer, il sen preleve une
        {
            yield return new WaitForSeconds(1); //Attend une seconde
            timer -= 1;
            derniereCoroutine = StartCoroutine(TimerSecoule()); //Sapelle lui même, agis comme une loop
        }

        if (timer == 0) 
        {

            if (derniereRuneTouche != null)
            {
                GameObject[] mesRunes = GameObject.FindGameObjectsWithTag("Rune");
                int i = 0;

                while (i < mesRunes.Length)
                {
                    mesRunes[i].GetComponent<TimerRune>().Activer();
                    i++;
                }

                if (derniereCoroutine != null)
                {
                    StopCoroutine(derniereCoroutine);
                }

                derniereRuneTouche = null;
                ResetTimer(0);
            }

            if (derniereRuneTouche != null)
            {
                peutResetRunes = false;
                GameObject[] mesRunes = GameObject.FindGameObjectsWithTag("Rune");

                int i = 0;
                while (i <= mesRunes.Length)
                {
                    mesRunes[i].GetComponent<TimerRune>().Activer();
                    i++;
                }
            }
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
