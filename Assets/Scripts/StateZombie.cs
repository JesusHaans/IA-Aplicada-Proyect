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
    public float smoothTime = 0.1f; // Tiempo de suavizado
    private Vector3 currentVelocity = Vector3.zero; // Velocidad actual de suavizado


    public float circleDistance = 30f; // distancia entre la partícula y el círculo
    public float angleStep = 1f; // tamaño del incremento aleatorio del ángulo

    private Vector3 circlePosition;
    private float angle;
    private Vector3 displacement;



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
                //Debug.Log("Posicion del jugador: " + playerPosition.position);
                SetState(State.SEEK);
            }
        }
    }

    // Funciones de actualizacion especificas para cada estado
    void UpdateWANDER()
    {
        //Logica del estado Wander
        Debug.Log(GetComponent<Rigidbody>().velocity.normalized);
        // Copia la velocidad de la partícula y normalízala
        circlePosition = GetComponent<Rigidbody>().velocity.normalized;

        // Multiplica por la distancia del círculo para establecer la posición del círculo
        circlePosition *= circleDistance;

        // Incrementa el ángulo con algún valor aleatorio en cada paso
        angle += Random.Range(0f, angleStep) - angleStep * 0.5f;

        // Establece la dirección inicial
        displacement = new Vector3(0, -1, 0);

        // Multiplica por la distancia del círculo para establecer la magnitud de la dirección
        displacement *= circleDistance;

        // Rota la dirección según el ángulo de wander
        displacement = Quaternion.Euler(0, 0, angle) * displacement;

        // Calcula la posición deseada
        Vector3 desired = circlePosition + displacement;

        // Utiliza 'desired' según tus necesidades (puede ser la dirección de movimiento del NPC)
        // Por ejemplo, podrías utilizar 'desired' para aplicar una fuerza al objeto.
        GetComponent<Rigidbody>().AddForce(desired.normalized * normalSpeed * Time.deltaTime);

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
        //Vector3 steeringForce = normalSpeed * Time.deltaTime * desiredDirection;
        Vector3 steeringForce = new Vector3(0, 0, desiredDirection.z * Time.deltaTime * (normalSpeed * -1));
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
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position,radio);
    }

}
