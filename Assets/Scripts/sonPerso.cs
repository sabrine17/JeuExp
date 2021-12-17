using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class sonPerso : MonoBehaviourPunCallbacks
{
    public AudioClip pas1;
    public AudioClip pas2;
    public AudioClip pas3;
    public AudioClip sonSaut;

    GameObject joueur;
    AudioSource audioJoueur;

    bool peutjouerPas = true;
    bool aJoueSaut = false;

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
                peutjouerPas = false;
            }
        }

        else
        {
            StopCoroutine("pasJoueAudio");
            peutjouerPas = true;
        }

        if (joueur.GetComponent<MouvementPerso>().jumpCount == 0 && aJoueSaut == false)
        {

        }

    }
    IEnumerator pasJoueAudio()
    {
        if (joueur.GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            int nombre = Random.Range(1, 4);

            if (nombre == 1)
            {
                AudioClip pasAJouer = pas1;

                // photonView.RPC("JoueAudio", RpcTarget.All, pasAJouer);
                JoueAudio(pasAJouer);
            }

            else if (nombre == 2)
            {
                AudioClip pasAJouer = pas2;

                // photonView.RPC("JoueAudio", RpcTarget.All, pasAJouer);
                JoueAudio(pasAJouer);
            }

            else if (nombre == 3)
            {
                AudioClip pasAJouer = pas3;

                //photonView.RPC("JoueAudio", RpcTarget.All, pasAJouer);
                JoueAudio(pasAJouer);
            }

            else if (nombre == 4)
            {
                AudioClip pasAJouer = pas3;

                //photonView.RPC("JoueAudio", RpcTarget.All, pasAJouer);
                JoueAudio(pasAJouer);
            }
        }

        yield return new WaitForSeconds(delai);

        StartCoroutine("pasJoueAudio");
    }

    public void sautJoueAudio()
    {
        JoueAudio(sonSaut);
    }

    //[PunRPC]
    void JoueAudio(AudioClip audioAJouer)
    {
        //audioJoueur.clip = audioAJouer;
        audioJoueur.pitch = (Random.Range(0.7f, 1.3f));
        audioJoueur.volume = (Random.Range(0.3f, 0.6f));
        audioJoueur.PlayOneShot(audioAJouer);
    }
}
