using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateZombie : MonoBehaviour
{
    //Enum para los estados
    public enum State
    {
        WANDER,
        SEEK,
        ATACAR

    }
    private State currentState;
    public Collider[] sensor;
    public float radio;
    public Transform playerPosition;
    public GameObject player;
    public NavMeshAgent myNav;
    public float range;
    public Transform centerNav;




    // Start is called before the first frame update
    void Start()
    {
        SetState(State.WANDER);
        myNav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // L�gica de actualizaci�n en el estado actual
        switch(currentState)
        {
            case State.WANDER:
                UpdateWANDER();
                break;
            case State.SEEK:
                UpdateSEEK();
                break;
            case State.ATACAR:
                UpdateATACAR();
                break;
        // Se agregan mas casos segun sea necesario
        }

        sensor = Physics.OverlapSphere(this.transform.position, radio);
        foreach(var coll in sensor)
        {
            if(coll.gameObject.tag == "Player")
            {
                playerPosition = coll.gameObject.transform;
                //Debug.Log("Posicion del jugador: " + playerPosition.position);
                SetState(State.SEEK);
            }
        }
    }

    // Funciones de actualizacion especificas para cada estado
    void UpdateWANDER()
    {
        if(myNav.remainingDistance <= myNav.stoppingDistance)
        {
            Vector3 point;
            if(RandomPoint(centerNav.position,range,out point))
            {
                myNav.SetDestination(point);
            }
        }
    }
    void UpdateSEEK()
    {
        
        myNav.SetDestination(player.transform.position);
        SetState(State.WANDER);


       
    }
    void UpdateATACAR()
    {
        //Logica del estado ATACAR
    }

    // Funci�n para cambiar de estado
    void SetState(State newState)
    {
        currentState = newState;
        // Aqui se pueden agregar acciones adicionales al cambio de estado.
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomPoint,out hit, 1f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;

        }
        result = Vector3.zero;
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position,radio);
    }

}
