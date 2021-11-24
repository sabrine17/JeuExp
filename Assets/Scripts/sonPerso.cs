using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sonPerso : MonoBehaviour
{
    public AudioClip pas1;
    public AudioClip pas2;
    public AudioClip pas3;

    GameObject joueur;
    AudioSource audioJoueur;

    bool peutjouerPas = true;

    public float delai;

    // Start is called before the first frame update
    void Start()
    {
        joueur = gameObject;
        audioJoueur = joueur.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (joueur.GetComponent<Rigidbody2D>().velocity.x != 0)
        {
            if (peutjouerPas == true)
            {
                StartCoroutine("pasJoueAudio");
                print("wesh");
                peutjouerPas = false;
            }
        }

        else
        {
            StopCoroutine("pasJoueAudio");
        }

        if (joueur.GetComponent<Rigidbody2D>().velocity.y != 0)
        {
            StopCoroutine("pasJoueAudio");
        }
    }
    IEnumerator pasJoueAudio()
    {
    
        int nombre = Random.Range(1, 4);

        if (nombre == 1)
        {
            AudioClip pasAJouer = pas1;

            JoueAudio(pasAJouer);
        }

        else if (nombre == 2)
        {
            AudioClip pasAJouer = pas2;

            JoueAudio(pasAJouer);
        }

        else if (nombre == 3)
        {
            AudioClip pasAJouer = pas3;

            JoueAudio(pasAJouer);
        }

        else if (nombre == 4)
        {
            AudioClip pasAJouer = pas3;

            JoueAudio(pasAJouer);
        }

        yield return new WaitForSeconds(delai);

        StartCoroutine("pasJoueAudio");
    }

    void JoueAudio(AudioClip audioAJouer)
    {
        //audioJoueur.clip = audioAJouer;
        audioJoueur.pitch = (Random.Range(0.7f, 1.3f));
        audioJoueur.volume = (Random.Range(0.5f, 1f));
        audioJoueur.PlayOneShot(audioAJouer);
    }
}
