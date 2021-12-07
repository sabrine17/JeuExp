using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanismeNiveau1 : MonoBehaviour
{
    private GameObject perso;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D colision)
    {
        if (colision.gameObject.tag == "Danger")
        {
            GetComponent<Collider2D>().enabled = false;
            print("Collider s'efface");
        }
    }
}
