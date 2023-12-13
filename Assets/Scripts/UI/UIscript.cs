using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UIscript : MonoBehaviour
{
    public GameObject winner;
    public GameObject[] zombies;
    public GameObject jugador;

    // Start is called before the first frame update
    void Start()
    {
        zombies = GameObject.FindGameObjectsWithTag("zombie");
        winner = GameObject.Find("Canvas").transform.Find("Winner Screen").gameObject;
        jugador = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            winner.SetActive(true);
            foreach(GameObject z in zombies)
            {
                z.GetComponent<StateZombie>().enabled = false;
                z.GetComponent<NavMeshAgent>().enabled = false;
            }
            jugador.GetComponent<movePlayer>().enabled = false;
        }
    }
}
