using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class nav : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent myNav;

    // Start is called before the first frame update
    void Start()
    {
        myNav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        myNav.SetDestination(player.transform.position);
    }
}
