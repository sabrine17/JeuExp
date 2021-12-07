using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionRunes : MonoBehaviour
{
    GameObject derniereRuneTouche;

    public bool runeSaut = false;

    int timer = 0;
    public Text txtTimer;
    public GameObject positionTimer;


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
            txtTimer.text = "";


            if (derniereRuneTouche != null)
            {
                derniereRuneTouche.GetComponent<TimerRune>().Activer();
            }
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

    void RuneSaut()
    {
        timer = 0;
        StopCoroutine(TimerSecoule());
        timer = 3;
        StartCoroutine(TimerSecoule());
        derniereRuneTouche.GetComponent<TimerRune>().Desactiver();
        runeSaut = true;
    }

    IEnumerator TimerSecoule()
    {
        if (timer > 0)
        {
            yield return new WaitForSeconds(1);
            print(timer);
            timer -= 1;
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
