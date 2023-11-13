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
    public float wanderRadius = 5.0f; // Radio del área de deambulación
    public float normalSpeed = 1.0f;  // Velocidad normal de movimiento
    public float smoothTime = 0.5f; // Tiempo de suavizado
    private Vector3 currentVelocity = Vector3.zero; // Velocidad actual de suavizado
    private Vector3 wanderTarget;
    // Start is called before the first frame update
    void Start()
    {
        SetState(State.WANDER);
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
                Debug.Log("Posicion del jugador: " + playerPosition.position);
                SetState(State.SEEK);
            }
        }
    }

    // Funciones de actualizacion especificas para cada estado
    void UpdateWANDER()
    {
        //Logica del estado Wander
        GetNewWanderTarget();
        Vector3 desiredDirection = (wanderTarget - transform.position).normalized;
        this.transform.Translate(desiredDirection * normalSpeed * Time.deltaTime);


    }
    void UpdateSEEK()
    {
        //Logica del estado SEEK
        //this.transform.position = Vector3.MoveTowards(this.transform.position, playerPosition.position, normalSpeed*Time.deltaTime);
        //this.transform.LookAt(playerPosition);
        Vector3 toPlayer = playerPosition.position - this.transform.position;
        float predictionTime = toPlayer.magnitude / normalSpeed;
        Vector3 futurePlayerPosition = playerPosition.position + playerPosition.GetComponent<Rigidbody>().velocity * predictionTime;
        Vector3 desiredDirection = (futurePlayerPosition - this.transform.position).normalized;
        Vector3 steeringForce = normalSpeed * Time.deltaTime * desiredDirection;
        Vector3 smoothedDirection = Vector3.SmoothDamp(this.transform.forward,desiredDirection,ref currentVelocity, smoothTime);
        this.transform.rotation = Quaternion.LookRotation(smoothedDirection);
        this.transform.Translate(steeringForce);
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
    void GetNewWanderTarget()
    {
        Vector2 randomCirclePoint = Random.insideUnitCircle * wanderRadius;
        wanderTarget = new Vector3(randomCirclePoint.x, 0f, randomCirclePoint.y) + transform.position;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position,radio);
    }

}
