using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class verificationPorte : MonoBehaviour
{

    bool toucheJoueur2;
    bool toucheJoueur1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (toucheJoueur1 == true && toucheJoueur2 == true)
        {
            GameObject.Find("PersoPrincipal_1(Clone)").GetComponent<MecanismeNiveau1>().peutAvancer = true;
            GameObject.Find("PersoSecondaire_1(Clone)").GetComponent<MecanismeNiveau1>().peutAvancer = true;
            print("wesh");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PersoPrincipal_1(Clone)")
        {
            toucheJoueur1 = true;
            print("wesh");
        }

        if (collision.gameObject.name == "PersoSecondaire_1(Clone)")
        {
            print("wesh");
            toucheJoueur2 = true;
        }
    }
}
