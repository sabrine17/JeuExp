using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionRunes : MonoBehaviour
{
    GameObject derniereRuneTouche;

    public bool runeSaut = false;

    int timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer == 0)
        {
            runeSaut = false;
            if (derniereRuneTouche != null)
            {
                derniereRuneTouche.GetComponent<TimerRune>().Activer();
            }
        }

    }

    void RuneSaut()
    {
        timer = 0;
        timer = 3;
        StartCoroutine(TimerSecoule());
        derniereRuneTouche.GetComponent<TimerRune>().Desactiver();
        runeSaut = true;
    }

    IEnumerator TimerSecoule()
    {
        if (timer > 0)
        {
            print(timer);
            timer -= 1;
            yield return new WaitForSeconds(1);
            StartCoroutine(TimerSecoule());
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "RuneSaut")
        {
            derniereRuneTouche = collision.gameObject;
            derniereRuneTouche.GetComponent<TimerRune>().desactive = true;
            RuneSaut();
        }
    }
}
