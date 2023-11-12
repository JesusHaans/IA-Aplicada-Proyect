using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float normalSpeed = 1.0f;  // Velocidad normal de movimiento
    // Start is called before the first frame update
    void Start()
    {
        SetState(State.WANDER);
    }

    // Update is called once per frame
    void Update()
    {
        // Lógica de actualización en el estado actual
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
                Debug.Log("Posicion del jugador: " + playerPosition.position);
                SetState(State.SEEK);
            }
        }
    }

    // Funciones de actualizacion especificas para cada estado
    void UpdateWANDER()
    {
        //Logica del estado Wander


    }
    void UpdateSEEK()
    {
        //Logica del estado SEEK
        this.transform.position = Vector3.MoveTowards(this.transform.position, playerPosition.position, normalSpeed*Time.deltaTime);
        this.transform.LookAt(playerPosition);
        SetState(State.WANDER);
    }
    void UpdateATACAR()
    {
        //Logica del estado ATACAR
    }

    // Función para cambiar de estado
    void SetState(State newState)
    {
        currentState = newState;
        // Aqui se pueden agregar acciones adicionales al cambio de estado.
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position,radio);
    }

}
